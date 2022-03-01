using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private MovementOptions _options;
    [SerializeField] private StopDetector _stopDetector;
    [SerializeField] private GameObject _model;

    readonly private float _rotationAngle = 45f;

    private Player _player;
    private PlayerInput _input;
    private bool _canMoving = false;
    private bool _isNearEnemy = false;
    private bool _isNearObstacle = false;

    public float MoveSpeed => _options.MoveSpeed;
    public float StopDistance => _options.StopDistance;
    public float AttackDistance => _options.AttackDistance;
    public bool IsNearEnemy => _isNearEnemy;
    public bool IsNearObstacle => _isNearObstacle;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _player.MovingStarted += OnMovingStarted;
        _player.MovingStopped += OnMovingStopped;
    }

    private void OnDisable()
    {
        _player.MovingStarted -= OnMovingStarted;
        _player.MovingStopped -= OnMovingStopped;
    }

    private void Update()
    {
        _isNearEnemy = _stopDetector.IsNearEnemy(_options.StopDistance);
        _isNearObstacle = _stopDetector.IsNearObstacle(_options.StopDistance);
        
        if (_canMoving) 
        {
            if (_isNearEnemy == false && _isNearObstacle == false)
                Move();
        } 
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(_model.transform.rotation, targetRotation, Time.deltaTime * _options.LookSpeed);

        _model.transform.rotation = lookAt;
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _options.MoveSpeed * Time.deltaTime);

        Swipe();
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

    private void Rotate(float directionY)
    {
        var currentRotation = _model.transform.rotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, directionY * _rotationAngle, 0));
        _model.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, _options.RotationSpeed * Time.deltaTime);
    }
 
    private void OnMovingStarted()
    {
        _canMoving = true;;
    }

    private void OnMovingStopped()
    {
        _canMoving = false;
    }
}