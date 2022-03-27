using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class RocketLauncher : Weapon, IShooteable
{
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private GameObject _muzzleflare;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _reloadingTime = 0.5f;
    [SerializeField] private float _speed;

    private bool _canShoot = true;
    private Quaternion _startRotation;

    public override event UnityAction Shooted;
    public event UnityAction<float> Reloaded;

    private void OnValidate()
    {
        _reloadingTime = Mathf.Clamp(_reloadingTime, 0, float.MaxValue);
    }

    private void Start()
    {
        _startRotation = transform.rotation;
    }

    public void TryShoot()
    {
        if (_canShoot)
            Shoot();
    }

    public new void Shoot()
    {
        ITarget target = _targetDetector.GetMaxHealthTarget(transform.position);

        if (target != null)
            TurnToTarget(target.Position);
        else
            transform.rotation = _startRotation;

        Instantiate(_muzzleflare, _shootPoint.position, _shootPoint.rotation);

        Rigidbody rocketInstance = Instantiate(_bullet.GetComponent<Rigidbody>(), _shootPoint.position, _shootPoint.rotation) as Rigidbody;

        rocketInstance.AddForce(_shootPoint.forward * _speed);

        Shooted?.Invoke();
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        _canShoot = false;
        Reloaded?.Invoke(_reloadingTime);

        yield return new WaitForSeconds(_reloadingTime);

        _canShoot = true;
    }

    public new void TurnToTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = targetRotation;
    }
}