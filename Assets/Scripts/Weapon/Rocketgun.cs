using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rocketgun : Weapon, IShooteable
{
    [SerializeField] private List<Transform> _shootPoints;
    [SerializeField] private GameObject _muzzleflare;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private DamageCollider _bullet;
    [SerializeField] private float _offsetRotationX;
    [SerializeField] private float _shootDistance = 25;

    private float _shootingTimer;
    private ITarget _target;
    private ITarget _thisITarget;
    private Quaternion _offsetRotation;

    public override event UnityAction Shooted;

    private void Start()
    {
        _offsetRotation = Quaternion.Euler(_offsetRotationX, 0f, 0f);
    }
    private void Update()
    {
        if (IsShooting)
        {
            if (IsAiming)
            {
                LookAtTarget(TargetPosition);

                Reload();

                _shootingTimer += Time.deltaTime;
            }
            else if (_thisITarget.IsDied == false)
            {
                if (_target.IsDied)
                {
                    StopShooting();
                }
                else
                {
                    if (Vector3.Distance(transform.position, _target.Position) <= _shootDistance)
                    {
                        TurnToTarget(_target.Position);

                        Reload();
                    }
                }
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
        for (int i = 0; i < _shootPoints.Count; i++)
        {
            Transform shootPoint = _shootPoints[i];

            Instantiate(_muzzleflare, shootPoint.position, shootPoint.rotation);

            Rigidbody bullet = Instantiate(_bullet.GetComponent<Rigidbody>(), shootPoint.position, shootPoint.rotation) as Rigidbody;
            
            if(_target != null)
                bullet.GetComponent<RocketProjectile>().Init(_target);
            else
                bullet.AddForce(shootPoint.forward * Random.Range(_minSpeed, _maxSpeed));
        }

        Shooted?.Invoke();
    }

    public new void TurnToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation *= _offsetRotation, Time.deltaTime * _lookSpeed);

        transform.rotation = lookAt;
    }

    private void LookAtTarget(Vector3 target)
    {
        Vector3 direction = TargetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation *= _offsetRotation;
        transform.rotation = targetRotation;
    }

    private void Reload()
    {
        if (_shootingTimer >= _cooldownTime)
        {
            Shoot();
            _shootingTimer = 0;
        }

        _shootingTimer += Time.deltaTime;
    }
}