using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, IDamageable, ITarget, IAward
{
    [SerializeField] private Health _health;
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _award;
    [SerializeField] private float _offsetY;

    private Vector3 _offset => new Vector3(0f, _offsetY, 0f);

    private ITarget _target; 
    
    public ITarget Target => _target;
    public Health Health => _health;
    public int Award => _award;
    public bool IsDied => _health.Value <= 0;
    public Vector3 Position => transform.position + _offset;

    public event UnityAction<IDamageable> Died;
    public event UnityAction Shooted;
    public event UnityAction TargetLost;
    public event UnityAction Moved;
    public event UnityAction Stopped;

    private void OnEnable()
    {
        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].Shooted += OnShooted;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].Shooted -= OnShooted;
    }

    public void Init(ITarget target)
    {
        _target = target;
    }

    public void StopEnemy()
    {
        StopShooting();
        Stopped?.Invoke();
    }

    public void ContinueAttack()
    {
        InitTargetForWeapon();
        Moved?.Invoke();
    }

    public void LoseTarget()
    {
        _target = null;
        TargetLost?.Invoke();
    }

    public void InitTargetForWeapon()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
           if(_weapons[i] is IShooteable shooteable)
                shooteable.SetTarget(Target, this);
        }
    }

    public void StopShooting()
    {
        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].StopShooting();
    }

    protected void OnShooted()
    {
        Shooted?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (IsDied)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}