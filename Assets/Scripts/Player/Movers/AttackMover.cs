using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(MoverOptions))]
public class AttackMover : State, IMover
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private TargetDetector _targetDetector;

    private Player _player;
    private MoverOptions _moverOptions;
    private bool _isMoving = true;
    private ITarget _target;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _moverOptions = GetComponent<MoverOptions>();
    }

    private void Update()
    {
        if (_moverOptions.IsNearEnemy || _moverOptions.IsNearObstacle)
        {
            if (_target == null || _target.IsDied)
            {
                _target = _targetDetector.GetClosestTarget(transform.position);
            }
            else if (_target.IsDied == false)
            {
                LookAtTarget(_target.Position);

                if (Vector3.Distance(_target.Position, transform.position) > _moverOptions.AttackDistance)
                    Move();
                else
                    Attack();
            }
        }
        else
        {
            StartMoving();
        }
    }

    public void Move()
    {
        Vector3 targetPosition = new Vector3(_target.Position.x, transform.position.y, _target.Position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moverOptions.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_target.Position, transform.position) <= _moverOptions.AttackDistance)
        {
            StopMoving();
        }
        else
        {
            if(_isMoving == false)
                StartMoving();
        }
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(_moverOptions.ModelRotation, targetRotation, Time.deltaTime * _moverOptions.LookSpeed);

        _moverOptions.RotateModel(lookAt);
    }

    private void Attack()
    {
        if (_isMoving)
            StopMoving();

        _attacker.Attack(false);

        if (_target == null)
        {
            _attacker.Reload();
            StartMoving();
        }
    }

    private void StartMoving()
    {
        _player.StartMove();
        _isMoving = true;
    }

    private void StopMoving()
    {
        _player.Stand();
        _isMoving = false;
    }
}