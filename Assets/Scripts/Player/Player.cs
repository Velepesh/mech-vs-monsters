using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable, ITarget
{
    [SerializeField] private float _deadAfterFellTime;

    private readonly int _damageToShakeCamera = 30;
    
    private Health _health = new Health();
    private int _speed;
    private int _attackForce;
    private FightType _fightType;

    public Vector3 Position => transform.position + new Vector3(0f, 1f, 0f);
    public Health Health => _health;
    public int AttackForce => _attackForce;
    public int Speed => _speed;
    public FightType FightType => _fightType;
    public bool IsDied => _health.Value <= 0;

    public event UnityAction LevelStarted;
    public event UnityAction DamageTook;
    public event UnityAction Fell;
    public event UnityAction Moved;
    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> MoneyAdded;
    public event UnityAction<int> SpeedChanged;
    public event UnityAction<int> AttackForceChanged;
    public event UnityAction Standed;
    public event UnityAction Won;
    public event UnityAction FightWon;
    public event UnityAction<Leg> LegChanged;
    public event UnityAction<Monster> Fought;
    public event UnityAction<Transform, Monster, FightType> Prepeared;
    public event UnityAction AnimatorStopped;
    public event UnityAction AnimatorStarted;

    public void StartLevel()
    {
        _health.RecordHealth();
        StartMove();

        LevelStarted?.Invoke();
    }

    public void Fall()
    {
        Fell?.Invoke();

        StartCoroutine(DeadAfterFell(_deadAfterFellTime));
    }

    public void StartAnimation()
    {
        AnimatorStarted?.Invoke();
    }

    public void StopAnimation()
    {
        AnimatorStopped?.Invoke();
    }

    public void ChangeLeg(Leg leg)
    {
        LegChanged?.Invoke(leg);
    }

    public void StartMove()
    {
        Moved?.Invoke();
    }

    public void Win()
    {
        Won?.Invoke();
    }

    public void WinInFight()
    {
        FightWon?.Invoke();

        StartMove();
    }

    public void AddAttackForce(int attackForce)
    {
        _attackForce += attackForce;
        AttackForceChanged?.Invoke(_attackForce);
    }

    public void RemoveAttackForce(int attackForce)
    {
        if (attackForce > _attackForce)
            _attackForce = 0;
        else
            _attackForce -= attackForce;

        AttackForceChanged?.Invoke(_attackForce);
    }

    public void AddMoney(int money)
    {
        MoneyAdded?.Invoke(money);
    }

    public void Fight(Monster monster)
    {
        _health.RestoreHealth();
        Fought?.Invoke(monster);
    }
    
    public void PrepearedForFight(Transform targetPoint, Monster monster, FightType type)
    {
        _fightType = type;
        Prepeared?.Invoke(targetPoint, monster, type);
    }

    public void Stand()
    {
        Standed?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (_damageToShakeCamera <= damage)
            DamageTook?.Invoke();

        _health.TakeDamage(damage);

        if (IsDied)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
    }

    public void LoadSpeed(int speed)
    {
        _speed = speed;

        SpeedChanged?.Invoke(speed);
    }

    private IEnumerator DeadAfterFell(float time)
    {
        yield return new WaitForSeconds(time);

        TakeDamage(_health.Value);
    }
}