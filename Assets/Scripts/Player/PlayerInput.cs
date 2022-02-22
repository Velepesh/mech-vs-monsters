using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
   [SerializeField] private float _sensitivity;

    private readonly float _minMouseMove = 3f;

    private float _mousePositionX;
    private float _moveFactorX;
    private float _previousMoveFactorX;
    private bool _isFight;
    private Player _player;

    public float MoveFactorX => _moveFactorX;
    public float Sensitivity => _sensitivity;

    public event UnityAction Attacked;

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
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (_isFight)
                    Attacked?.Invoke();
                else
                    _mousePositionX = Input.mousePosition.x;
            }
        }
        else if (Input.GetMouseButton(0))
        {
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

    private void OnFought(Godzilla godzilla)
    {
        _isFight = true;
    }
}