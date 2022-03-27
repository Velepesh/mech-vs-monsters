using UnityEngine;
using UnityEngine.Events;

public class Machinegun : Weapon, IShooteable
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _muzzleflare;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private DamageCollider _bullet;
    [SerializeField] private float _offsetRotationX;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _shootDistance = 31;

    private float _shootingTimer;
    private ITarget _target;
    private ITarget _thisITarget;
    private Quaternion _offsetRotation;

    public override event UnityAction Shooted;

    private void Start()
    {
        _shootingTimer = _cooldownTime;
        _offsetRotation = Quaternion.Euler(_offsetRotationX, 0f, 0f);
    }

    private void Update()
    {
        if (IsShooting && _thisITarget.IsDied == false)
        {
            if (_target.IsDied)
            {
               StopShooting();
            }
            else if (Vector3.Distance(transform.position, _target.Position) <= _shootDistance)
            {
                TurnToTarget(_target.Position + _offset);

                if (_shootingTimer >= _cooldownTime)
                {
                    Shoot();
                    _shootingTimer = 0;
                }

                _shootingTimer += Time.deltaTime;
            }
        }
    }

    public new void SetTarget(ITarget target, ITarget thisDamageable)
    {
        _target = target;
        _thisITarget = thisDamageable;
        StartShooting();
    }

    public new void Shoot()
    {
        Instantiate(_muzzleflare, _shootPoint.position, _shootPoint.rotation);

        Rigidbody bullet = Instantiate(_bullet.GetComponent<Rigidbody>(), _shootPoint.position, _shootPoint.rotation) as Rigidbody;

        bullet.AddForce(_shootPoint.forward * Random.Range(_minSpeed, _maxSpeed));

        Shooted?.Invoke();
    }

    public new void TurnToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation *= _offsetRotation, Time.deltaTime * _lookSpeed);
        
        transform.rotation = lookAt;
    }
}