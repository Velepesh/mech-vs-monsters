using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable, ITarget, IDieingPolicy
{
    private readonly int _award = 0;
    
    private int _health;
    private int _speed;
    private int _attackForce;
    private int _startHealth;
    public Vector3 Position => transform.position + new Vector3(0f, 1f, 0f);

    public event UnityAction LevelStarted;
    public event UnityAction MovingStarted;
    public event UnityAction MovingStopped;
    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> MoneyAdded;
    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> SpeedChanged;
    public event UnityAction<int> AttackForceChanged;
    public event UnityAction Standed;
    public event UnityAction Won;
    public event UnityAction<Leg> LegChanged;
    public event UnityAction<Godzilla> Fought;
    public event UnityAction<Transform, Godzilla> Prepeared;

    public int Health => _health;
    public int AttackForce => _attackForce; 
    public int Speed => _speed;
    public int Award => _award;
    public int StartHealth => _startHealth;
    public bool IsDied => _health <= 0;

    public void StartLevel()
    {
        _startHealth = _health;
        StartMove();
        LevelStarted?.Invoke();
    } 
    
    public void ChangeLeg(Leg leg)
    {
        LegChanged?.Invoke(leg);
    }

    public void StartMove()
    {
        MovingStarted?.Invoke();
    }

    public void Win()
    {
        StopMoving();
        Won?.Invoke();
    }

    public void AddHealth(int health)
    {
        _health += health;
        HealthChanged?.Invoke(_health);
    }

    public void RemoveHealth(int health)
    {
        if (health > _health)
            _health = 0;
        else
            _health -= health;

        HealthChanged?.Invoke(_health);
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

    public void StopMoving()
    {
        MovingStopped?.Invoke();
    }

    public void Fight(Godzilla godzilla)
    {
        _health = _startHealth;
        Fought?.Invoke(godzilla);
    }
    
    public void PrepearedForFight(Transform targetPoint, Godzilla godzilla)
    {
        StopMoving();
        Prepeared?.Invoke(targetPoint, godzilla);
    }

    public void Stand()
    {
        StopMoving();
        Standed?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            _health = 0;

        HealthChanged?.Invoke(_health);
        
        if(_health == 0)
            Die();
    }

    public void Die()
    {
        StopMoving();
        Died?.Invoke(this);
    }

    public void LoadSpeed(int speed)
    {
        _speed = speed;

        SpeedChanged?.Invoke(speed);
    }

}