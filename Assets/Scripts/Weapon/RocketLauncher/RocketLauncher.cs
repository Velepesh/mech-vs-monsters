using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class RocketLauncher : Weapon, IShooteable
{
    [SerializeField] private GameObject _muzzleflare;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _reloadingTime = 0.5f;
    [SerializeField] private float _speed;

    private bool _canShoot = true;

    public override event UnityAction Shooted;
    public event UnityAction<float> Reloaded;

    private void OnValidate()
    {
        _reloadingTime = Mathf.Clamp(_reloadingTime, 0, float.MaxValue);
    }

    public void TryShoot()
    {
        if (_canShoot)
            Shoot();
    }

    public new void Shoot()
    {
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
}