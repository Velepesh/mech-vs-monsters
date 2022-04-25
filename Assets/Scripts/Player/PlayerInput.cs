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

    private readonly float _minMouseMove = 3f;

    private Vector3 _mousePosition;
    private float _moveFactorX;
    private Vector3 _aimPosition;

    private float _previousMoveFactorX;

    private bool _isFight;
    private bool _isHold;
    private Player _player;

    public float MoveFactorX => _moveFactorX;
    public Vector3 AimPosition => _aimPosition;
    public float Sensitivity => _sensitivity;
    public bool IsHold => _isHold;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _previousMoveFactorX = 0;
    }

    private void OnEnable()
    {
        _player.Fought += OnFought;
    }

    private void OnDisable()
    {
        _player.Fought -= OnFought;
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
                        if (_player.IsAiming == false)
                            _attacker.Attack(true);
                        else
                            _mousePosition = Input.mousePosition;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 position = Input.mousePosition;

                    RaycastHit hit;
                    Ray ray = _camera.ScreenPointToRay(position);
                    if (Physics.Raycast(ray, out hit, float.MaxValue))
                        _aimPosition = hit.point - transform.position;

                    _moveFactorX = position.x - _mousePosition.x;

                    ApplyMoveFactorX();

                    _mousePosition = position;
                }

                _isHold = true;
                _aimShooting.Shoot(Input.mousePosition);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _moveFactorX = 0f;

                _previousMoveFactorX = _moveFactorX;

                _isHold = false;

                if (_player.IsAiming)
                    _playerWeaponsHolder.StopShooting();
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
                    if (_player.IsAiming == false)
                        _attacker.Attack(true);
                }
                else
                {
                    _mousePosition = Input.mousePosition;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;

            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out hit, float.MaxValue))
                _aimPosition = hit.point - transform.position;

            _moveFactorX = position.x - _mousePosition.x;
            _aimShooting.Shoot(position);

            ApplyMoveFactorX();

            _isHold = true;
            _mousePosition = position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;

            _previousMoveFactorX = _moveFactorX;

            _isHold = false;

            if (_player.IsAiming)
                _playerWeaponsHolder.StopShooting();
        }
    }
    private void ApplyMoveFactorX()
    {
        if (_previousMoveFactorX < 0f && _moveFactorX < 0f)
            _moveFactorX = -1f;
        else if (_previousMoveFactorX > 0f && _moveFactorX > 0f)
            _moveFactorX = 1f;
        
        if (_moveFactorX == 0 || Mathf.Abs(_moveFactorX) < _minMouseMove)
            _moveFactorX = _previousMoveFactorX;

        _previousMoveFactorX = Mathf.Clamp(_moveFactorX, -1f, 1f);
    }

    private void OnFought(Monster monster)
    {
        _isFight = true;
    }
}