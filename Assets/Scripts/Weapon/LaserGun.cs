using UnityEngine;
using UnityEngine.Events;

public class LaserGun : Weapon
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private LayerMask _timerMonsterColliderLayerMask;
    [SerializeField] private LayerMask _hittedLayerMask;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _maxDistace;
    [SerializeField] private Aim _aim;
    [SerializeField] private CameraChanger _cameraChange;

    private TimerMonsterCollider _lastCollider;

    public override event UnityAction Shooted;
    
    private void Update()
    {
        if (IsShooting)
            Shoot();
        else
            _line.enabled = false;
    }

    public new void Shoot()
    {
        Ray inputRay = _cameraChange.Camera.ScreenPointToRay(_aim.Position);
        RaycastHit hit;

        _line.SetPosition(0, _shootPoint.position);

        if (Physics.Raycast(inputRay, out hit, _maxDistace, _timerMonsterColliderLayerMask))
        {
            _lastCollider = hit.transform.GetComponent<TimerMonsterCollider>();
           
            if (_lastCollider != null)
                _lastCollider.TakeDamage();

            _line.SetPosition(1, hit.point);
        }
        else if (Physics.Raycast(inputRay, out hit, _maxDistace, _hittedLayerMask))
        {
            if (hit.collider)
            {
                if (_lastCollider != null)
                {
                    _lastCollider.SwitchToDefaultState();
                }

                _line.SetPosition(1, hit.point);
            }
        }
        else
        {
            _line.SetPosition(1, transform.forward * _maxDistace);
        }

        _line.enabled = true;

        Shooted?.Invoke();
    }
}