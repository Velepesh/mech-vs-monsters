using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerWeaponsHolder))]
[RequireComponent(typeof(DownMover))]
[RequireComponent(typeof(PlayerInput))]
public class AimShooting : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Aim _aim;
    [SerializeField] private LayerMask _layerMask;

    private Player _player;
    private DownMover _downMover;
    private PlayerWeaponsHolder _playerWeaponsHolder;
    private PlayerInput _input;
    private bool _canShoot;

    public Player Player => _player;

    public event UnityAction Shooted;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _downMover = GetComponent<DownMover>();
        _playerWeaponsHolder = GetComponent<PlayerWeaponsHolder>();
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _downMover.Landed += OnLanded;
    }

    private void OnDisable()
    {
        _downMover.Landed -= OnLanded;
    }


    public void Shoot(Vector3 direction, Vector3 position)
    {
        if (_canShoot)
        {
            _aim.MoveAim(direction, position);

            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(_aim.Position);

            if (Physics.Raycast(ray, out hit, float.MaxValue, _layerMask))
            {
                for (int i = 0; i < _playerWeaponsHolder.Count; i++)
                {
                    Weapon weapon = _playerWeaponsHolder.GetWeapon(i);
                    weapon.SetTarget(hit.point);
                }
            }

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