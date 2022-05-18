using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(DownMover))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private AimShooting _aimShooting;
    [SerializeField] private float _sensitivity;
    [SerializeField] private bool _isMobileInput;

    private Vector3 _mousePosition;
    private Vector3 _swipePosition;

    private bool _isFight;
    private bool _isHandsFight;
    private bool _isHold;
    private Player _player;

    public Vector3 SwipePosition => _swipePosition;
    public float Sensitivity => _sensitivity;
    public bool IsHold => _isHold;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Prepeared += OnPrepeared;
        _player.FightWon += OnFightWon;
    }

    private void OnDisable()
    {
        _player.Prepeared -= OnPrepeared;
        _player.FightWon -= OnFightWon;
    }

    private void Update()
    {
        if (_isMobileInput)
            MobileControl();
        else
            MouseControl();
    }

    private void MobileControl()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (_isFight)
                    {
                        if (_isHandsFight)
                            _attacker.Attack(true);
                    }
                }

                _mousePosition = Input.mousePosition;

            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 position = Input.mousePosition;
                    Vector3 aimPosition = position - _mousePosition;

                    ApplySwipePosition(position);

                    _aimShooting.Shoot(aimPosition);

                    _mousePosition = position;
                }

                _isHold = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                _isHold = false;

                if (_isHandsFight == false)
                    _playerWeaponsHolder.StopWeaponShooting();
            }
            else
            {
                _mousePosition = Input.mousePosition;
            }
        }
    }

    private void MouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (_isFight)
                {
                    if (_isHandsFight)
                        _attacker.Attack(true);
                }
            }

            _mousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;
            Vector3 aimPosition = position - _mousePosition;
            _swipePosition = aimPosition;

            if (_isFight)
                _aimShooting.SetHandTarget(aimPosition);
            else
                _aimShooting.Shoot(aimPosition);

            _isHold = true;
            _mousePosition = position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isHold = false;

            if (_isHandsFight == false)
                _playerWeaponsHolder.StopWeaponShooting();

            _mousePosition = Input.mousePosition;
        }
        else
        {
            _mousePosition = Input.mousePosition;           
        }
    }

    private void ApplySwipePosition(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out hit, float.MaxValue))
            _swipePosition = hit.point - transform.position;
    }

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
    {
        _isFight = true;

        if (type == FightType.Hands)
            _isHandsFight = true;
    }

    private void OnFightWon()
    {
        _isFight = false;
        _isHandsFight = false;
    }
}