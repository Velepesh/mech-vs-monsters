using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerWeaponsHolder))]
[RequireComponent(typeof(DownMover))]
[RequireComponent(typeof(PlayerInput))]
public class AimShooting : MonoBehaviour
{
    [SerializeField] private CameraChanger _cameraChanger;
    [SerializeField] private Aim _aim;
    [SerializeField] private LazerArms _lookAt;
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
        _player.Prepeared += OnPrepeared;
        _player.FightWon += OnFightWon;
        _downMover.Landed += OnLanded;
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Prepeared -= OnPrepeared;
        _player.FightWon -= OnFightWon;
        _downMover.Landed -= OnLanded;
        _player.Won -= OnWon;
    }

    public void Shoot(Vector3 position)
    {
        if (_canShoot)
        {
            _aim.MoveAim(position);

            Ray ray = _cameraChanger.Camera.ScreenPointToRay(_aim.Position);
            SetWeaponsTarget(ray);

            Shooted?.Invoke();
        }
    }

    public void SetHandTarget(Vector3 position)
    {
        _aim.MoveAim(position);

        Ray ray = _cameraChanger.Camera.ScreenPointToRay(_aim.Position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _layerMask))
            _lookAt.SetTarget(hit.point);
    }

    private void SetWeaponsTarget(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, _layerMask))
        {
            for (int i = 0; i < _playerWeaponsHolder.DefaultWeapons.Count; i++)
            {
                Weapon weapon = _playerWeaponsHolder.GetDefaultWeapon(i);
                weapon.SetTarget(hit.point);
            }
        }
    }

    private void OnLanded()
    {
        EnableAimShooting();
    }

    private void OnFightWon()
    {
        EnableAimShooting();
    }

    private void OnPrepeared(Transform targetPoint, Monster monster, FightType type)
    {
        if (type == FightType.Hands)
            DisableAimShooting();
        else
            _aim.DisableAim();
    }

    private void OnWon()
    {
        EnableAimShooting();
    }

    private void EnableAimShooting()
    {
        _aim.ShowAim();
        _canShoot = true;
    }

    private void DisableAimShooting()
    {
        _aim.DisableAim();
        _canShoot = false;
    }
}