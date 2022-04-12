using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class RocketLauncher : Weapon, IShooteable
{
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private GameObject _muzzleflare;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private RocketProjectile _bullet;
    [SerializeField] private float _cooldownTime = 4f;

    private bool _canShoot = true;

    public override event UnityAction Shooted;
    public event UnityAction<float> Reloaded;

    private void OnValidate()
    {
        _cooldownTime = Mathf.Clamp(_cooldownTime, 0, float.MaxValue);
    }

    public void TryShoot()
    {
        if (_canShoot)
            Shoot();
    }

    public new void Shoot()
    {
        ITarget target = _targetDetector.GetMaxHealthTarget(transform.position);

        Instantiate(_muzzleflare, _shootPoint.position, _shootPoint.rotation);

        GameObject bullet = Instantiate(_bullet.gameObject, _shootPoint.position, _shootPoint.rotation);
        bullet.GetComponent<RocketProjectile>().Init(target);

        Shooted?.Invoke();
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _canShoot = false;
        Reloaded?.Invoke(_cooldownTime);

        yield return new WaitForSeconds(_cooldownTime);

        _canShoot = true;
    }

    public new void TurnToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = targetRotation;
    }
}