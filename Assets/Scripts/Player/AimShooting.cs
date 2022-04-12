using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerWeaponsHolder))]
[RequireComponent(typeof(DownMover))]
public class AimShooting : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Aim _aim;
    [SerializeField] private LayerMask _layerMask;

    private Player _player;
    private DownMover _downMover;
    private PlayerWeaponsHolder _playerWeaponsHolder;
    private bool _canShoot;

    public Player Player => _player;

    public event UnityAction Shooted;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _downMover = GetComponent<DownMover>();
        _playerWeaponsHolder = GetComponent<PlayerWeaponsHolder>();
    }

    private void OnEnable()
    {
        _downMover.Landed += OnLanded;
    }

    private void OnDisable()
    {
        _downMover.Landed -= OnLanded;
    }

    public void Shoot()
    {
        if (_canShoot)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, float.MaxValue, _layerMask))
            {
                for (int i = 0; i < _playerWeaponsHolder.Count; i++)
                {
                    Weapon weapon = _playerWeaponsHolder.GetWeapon(i);
                    weapon.SetTarget(hit.point);
                }
            }

            _aim.MoveAim(Input.mousePosition);

            Shooted?.Invoke();
        }
    }

    private void OnLanded()
    {
        if (_player.IsAiming)
        {
            _aim.ShowAim();
            _canShoot = true;
        }
    }
}