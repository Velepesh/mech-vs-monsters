using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IDamageable, ITarget, IAward
{
    [SerializeField] private Health _health;
    [SerializeField] private int _award;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _moveToDiePointTime = 0.25f;
    [SerializeField] private AnimationCurve _moveAnimation;
    [SerializeField] private Transform _targetDiePoint;
    private Vector3 _offset => new Vector3(0f, _offsetY, 0f);

    public int Award => _award;
    public Health Health => _health;
    public bool IsDied => _health.Value <= 0;
    public Vector3 Position => transform.position + _offset;

    public event UnityAction Won;
    public event UnityAction AttackStarted;
    public event UnityAction Attacked;
    public event UnityAction AttackStopped;
    public event UnityAction<IDamageable> Died;
    public event UnityAction<Monster> Disabled;

    private void Start()
    {
        Health.RecordHealth();
        DisableModel();
    }

    public void Fight(Player player)
    {
        EnableModel();
        AttackStarted?.Invoke();
    }

    public void Attack()
    {
        Attacked?.Invoke();
    }

    public void Win()
    {
        Won?.Invoke();
    }

    public void StopAttack()
    {
        AttackStopped?.Invoke();
    }

    public void EnableModel()
    {
        _model.SetActive(true);
    }

    private void DisableModel()
    {
        _model.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (IsDied)
            Die();
    }

    public void Die()
    {
        StartCoroutine(MoveToDiePoint(_moveToDiePointTime));
        DisableModel();
        Disabled?.Invoke(this);
    }

    private IEnumerator MoveToDiePoint(float duration)
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, _targetDiePoint.position, _moveAnimation.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void DsableMonster()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}