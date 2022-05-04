using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable, ITarget, IAward
{
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _award;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _speed;
    private Vector3 _offset => new Vector3(0f, _offsetY, 0f);

    private ITarget _target;
    private float _startPosition;
    private Vector3 _targetPosition;

    public event UnityAction<IDamageable> Died;
    public event UnityAction Shooted;
    public event UnityAction TargetLost;

    public IShooteable Weapon => _weapon;
    public ITarget Target => _target;
    public Health Health => _health;
    public int Award => _award;
    public float LookSpeed => _lookSpeed;
    public bool IsDied => _health.Value <= 0;
    public Vector3 Position => transform.position + _offset;

    private void OnEnable()
    {
        _weapon.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _weapon.Shooted -= OnShooted;
    }

    private void Update()
    {
        if (_target != null && Target.IsDied == false)
            Move();
    }

    private void Start()
    {
        _startPosition = transform.position.x;
    }

    public void Move()
    {
        TurnToTarget(Target.Position);

        _targetPosition = new Vector3(transform.position.x, transform.position.y, Target.Position.z + _distanceToPlayer);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);

        transform.rotation = lookAt;
    }

    public void Init(ITarget target)
    {
        _target = target;

        InitTargetForWeapon();
    }

    public void LoseTarget()
    {
        _target = null;
        TargetLost?.Invoke();
    }

    private void OnShooted()
    {
        Shooted?.Invoke();
    }

    private void InitTargetForWeapon()
    {
        if (_weapon is IShooteable shooteable)
            shooteable.SetTarget(Target, this);
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