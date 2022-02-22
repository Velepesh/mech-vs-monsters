using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable, ITarget, IDieingPolicy
{
    [SerializeField] private int _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _award;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private float _offsetY;
    private Vector3 _offset => new Vector3(0f, _offsetY, 0f);

    private ITarget _target;

    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> HealthChanged;
    public event UnityAction TargetInited;
    public event UnityAction TargetLost;

    public IShooteable Weapon => _weapon;
    public ITarget Target => _target;
    public int Health => _health;
    public int Award => _award;
    public float LookSpeed => _lookSpeed;
    public bool IsDied => _health <= 0;
    public Vector3 Position => transform.position + _offset;

    private void OnEnable()
    {
        _weapon.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _weapon.Shooted -= OnShooted;
    }

    public void Init(ITarget target)
    {
        _target = target;

        InitTargetForWeapon();
        TargetInited?.Invoke();
    }

    public void LoseTarget()
    {
        _target = null;
        TargetLost?.Invoke();
    }

    private void OnShooted()
    {
        TargetInited?.Invoke();
    }

    private void InitTargetForWeapon()
    {
        if (_weapon is IShooteable shooteable)
            shooteable.SetTarget(Target, this);
    }

    private void Update()
    {
        if (_target != null && Target.IsDied == false)
            TurnToTarget(Target.Position);
    }

    public void TurnToTarget(Vector3 target)
    {
        Vector3 direction = target + _offset - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);

        transform.rotation = lookAt;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            _health = 0;

        HealthChanged?.Invoke(_health);

        if (_health == 0)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
        gameObject.SetActive(false);
    }
}