using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private AimShooting _aimShooting;
    [SerializeField] private float _sensitivity;
    [SerializeField] private bool _isMobileInput;

    private readonly float _minMouseMove = 3f;

    private float _mousePositionX;
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
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        if (_isFight)
                            if (_player.IsAiming == false)
                                _attacker.Attack(true);
                            else
                                _mousePositionX = Input.mousePosition.x;
                    }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (_player.IsAiming)
                    _aimShooting.Shoot();

                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    float position = Input.mousePosition.x;
                    _moveFactorX = position - _mousePositionX;

                    ApplyMoveFactorX();

                    _previousMoveFactorX = Mathf.Clamp(_moveFactorX, -1f, 1f);
                    _mousePositionX = position;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _moveFactorX = 0f;
                _previousMoveFactorX = _moveFactorX;

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
                    if (_player.IsAiming == false)
                        _attacker.Attack(true);
                    else
                        _mousePositionX = Input.mousePosition.x;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (_player.IsAiming)
                _aimShooting.Shoot();

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                float position = Input.mousePosition.x;
                _moveFactorX = position - _mousePositionX;

                ApplyMoveFactorX();

                _previousMoveFactorX = Mathf.Clamp(_moveFactorX, -1f, 1f);
                _mousePositionX = position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            _previousMoveFactorX = _moveFactorX;

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
    }

    private void OnFought(Monster monster)
    {
        _isFight = true;
    }
}