using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(DownMover))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private AimShooting _aimShooting;
    [SerializeField] private float _sensitivity;
    [SerializeField] private bool _isMobileInput;

    private readonly float _minMouseMove = 3f;

    private Vector3 _mousePosition;
    private Vector3 _mousePositionAim;
    private Vector3 _aimPosition;
    private Vector3 _startPosition;
    private float _moveFactorX;

    private float _previousMoveFactorX;

    private bool _isFight;
    private Player _player;

    public float MoveFactorX => _moveFactorX;
    public float Sensitivity => _sensitivity;

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

                    _startPosition = Input.mousePosition;
                    _mousePositionAim = Input.mousePosition;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 direction = Vector3.zero;
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 position = Input.mousePosition;

                    _moveFactorX = position.x - _mousePosition.x;
                    Vector2 offset = position - _startPosition;
                    _aimPosition = position - _mousePositionAim;
                    direction = Vector2.ClampMagnitude(offset, 1f);

                    ApplyMoveFactorX();

                    _previousMoveFactorX = Mathf.Clamp(_moveFactorX, -1f, 1f);

                    _mousePosition = position;
                    _mousePositionAim = position;
                }

                _aimShooting.Shoot(direction, _aimPosition);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _moveFactorX = 0f;

                _previousMoveFactorX = _moveFactorX;

                if (_player.IsAiming)
                    _playerWeaponsHolder.StopShooting();
            }
        }
        else
        {
            _mousePositionAim = Input.mousePosition;
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

            _startPosition = Input.mousePosition;
            _mousePositionAim = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;

            _moveFactorX = position.x - _mousePosition.x;
            Vector2 offset = position - _startPosition;
            _aimPosition = position - _mousePositionAim;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1f);
            _aimShooting.Shoot(direction, _aimPosition);

            ApplyMoveFactorX();
           
            _previousMoveFactorX = Mathf.Clamp(_moveFactorX, -1f, 1f);

            _mousePosition = position;
            _mousePositionAim = position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;

            _previousMoveFactorX = _moveFactorX;

            if (_player.IsAiming)
                _playerWeaponsHolder.StopShooting();
        }
        else
        {
            _mousePositionAim = Input.mousePosition;
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
    }


    private void OnFought(Monster monster)
    {
        _isFight = true;
    }
}