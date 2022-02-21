using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovementOptions _options;
    [SerializeField] private GameObject _body;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private float _lookSpeed;

    readonly private float _rotationAngle = 45f;

    private Player _player;
    private ITarget _target;
    private PlayerInput _input;
    private Vector3 _godzillaPosition;
    private Vector3 _targetFightPosition;
    private bool _canMoving = false;
    private bool _isAttack = false;
    private bool _isFight = false;

    private void OnValidate()
    {
        _lookSpeed = Mathf.Clamp(_lookSpeed, 0, float.MaxValue); 
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
        _player.Prepeared += OnFought;
        _player.MovingStarted += OnMovingStarted;
        _player.MovingStopped += OnMovingStopped;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.Prepeared -= OnFought;
        _player.MovingStarted -= OnMovingStarted;
        _player.MovingStopped -= OnMovingStopped;
    }


    private void Update()
    {
        if (_canMoving)
        {              
            if(_targetDetector.IsStopDistance(_options.StopDistance, transform.position, out _target) == false)
            {
                Move();
                Swipe();
            }
            else
            {
                _isAttack = true;
            }
        }

        if (_isAttack)
        {
            if (_targetDetector.IsStopDistance(_options.StopDistance, transform.position, out _target))
            {
                TurnToTarget(_target.GetPosition(), _lookSpeed);

                if(Vector3.Distance(_target.GetPosition(), transform.position) > _options.AttackDistance)
                {
                    Vector3 targetPosition = new Vector3(_target.GetPosition().x, transform.position.y, _target.GetPosition().z);
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, _options.MoveSpeed * Time.deltaTime);
                    if (Vector3.Distance(_target.GetPosition(), transform.position) <= _options.AttackDistance)
                        _player.Stand();
                }
                else
                {
                    _player.Attack();

                    if (_target.IsDied)
                        _player.StartMove();
                }             
            }
            else
            {
                _player.StartMove();
                _isAttack = false;
            }
        }

        if (_isFight) 
        {
            if (_player.IsDied == false)
            {
                TurnToTarget(_godzillaPosition, _lookSpeed);
                transform.position = Vector3.MoveTowards(transform.position, _targetFightPosition, _options.MoveSpeed * Time.deltaTime);
            }
       }
    }

    private void Swipe()
    {
        float swerveAmount = Time.deltaTime * _input.Sensitivity * Mathf.Clamp(_input.MoveFactorX, -1f, 1);
        swerveAmount = Mathf.Clamp(swerveAmount, -_options.BorderOffset, _options.BorderOffset);
         
        if (swerveAmount > 0f)
            TryMoveRight(swerveAmount);
        else if (swerveAmount < 0f)
            TryMoveLeft(swerveAmount);
        else
            Rotate(0f);
    }

    private void TryMoveRight(float swerveAmount)
    {
        Vector3 position = transform.position;

        if (position.x >= _options.BorderOffset)
            ReachBorder(position, _options.BorderOffset);
        else
            MoveSideways(swerveAmount);
    }

    private void TryMoveLeft(float swerveAmount)
    {
        Vector3 position = transform.position;

        if (position.x <= -_options.BorderOffset)
            ReachBorder(position, -_options.BorderOffset);
        else
            MoveSideways(swerveAmount);
    }

    private void ReachBorder(Vector3 position, float border)
    {
        transform.position = new Vector3(border, position.y, position.z);
        Rotate(0f);
    }

    private void MoveSideways(float swerveAmount)
    {
        transform.Translate(swerveAmount, 0, 0);
        Rotate(Mathf.Clamp(_input.MoveFactorX, -1f, 1));
    }

    private void OnFought(Transform targetPoint, Godzilla godzilla)
    {
        OnMovingStopped();
        _godzillaPosition = godzilla.GetPosition();
        _targetFightPosition = targetPoint.position;
        _targetFightPosition.y = transform.position.y;
        _isFight = true;
        _isAttack = false;
    }
   
    private void OnWon()
    {
        OnMovingStopped();
        _isAttack = false;
        _isFight = false;
    }

    private void OnMovingStarted()
    {
        _canMoving = true;
        _isFight = false;
    }

    private void OnMovingStopped()
    {
        _canMoving = false;
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _options.MoveSpeed * Time.deltaTime);
    }

    private void Rotate(float directionY)
    {
        var currentRotation = _body.transform.rotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, directionY * _rotationAngle, 0));
        _body.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _options.RotationSpeed * Time.deltaTime);
    }

    public void TurnToTarget(Vector3 target, float speed)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(_body.transform.rotation, targetRotation, Time.deltaTime * speed);

        _body.transform.rotation = lookAt;
    }
}