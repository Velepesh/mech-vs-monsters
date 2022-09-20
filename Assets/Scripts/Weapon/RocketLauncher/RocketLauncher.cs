using UnityEngine.Events;
using UnityEngine;

public class RocketLauncher : Weapon, IShooteable
{
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private ParticleSystem _muzzleflare;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private RocketProjectile _bullet;
    [SerializeField] private float _cooldownTime = 4f;

    private float _shootingTimer;

    public override event UnityAction Shooted;
    public event UnityAction<float> Reloaded;

    private void OnValidate()
    {
        _cooldownTime = Mathf.Clamp(_cooldownTime, 0, float.MaxValue);
    }

    private void Update()
    {
        if (IsShooting)
        {
            Reload();
        }
    }

    public new void Shoot()
    {
        ITarget target = _targetDetector.GetClosetEnemy(transform.position);

        Instantiate(_muzzleflare.gameObject, _shootPoint.position, _shootPoint.rotation);

        GameObject bullet = Instantiate(_bullet.gameObject, _shootPoint.position, _shootPoint.rotation);
        bullet.GetComponent<RocketProjectile>().Init(target);

        Shooted?.Invoke();
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

    public new void TurnToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = targetRotation;
    }
}