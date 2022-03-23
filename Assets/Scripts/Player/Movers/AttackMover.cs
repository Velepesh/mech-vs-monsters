using UnityEngine;
using UnityEngine.Events;

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

    public event UnityAction Moved;
    private void Awake()
    {
        _player = GetComponent<Player>();
        _moverOptions = GetComponent<MoverOptions>();
    }

    private void Update()
    {
        _target = _targetDetector.GetClosestTarget(transform.position);

        if (_target != null && _player.IsDied == false)
        {
            Debug.Log("_target != null && _player.IsDied == false");
            LookAtTarget(_target.Position);

            if (Vector3.Distance(_target.Position, transform.position) > _moverOptions.AttackDistance)
            {
                Move();
                Debug.Log("Move");
            }
            else
            {
                if (_isMoving)
                {
                    _isMoving = false;
                    _player.Stand();
                }

                if (_target is Mine == false)
                {
                    Debug.Log("ATTACK");
                }
                    _attacker.Attack(false);

                if (_target == null)
                {
                    _attacker.Reload();
                    StartMove();
                }
            }
        }
        else
        {
            Debug.Log("ELSE");
            StartMove();
        }
    }

    public void Move()
    {
        Vector3 targetPosition = new Vector3(_target.Position.x, transform.position.y, _target.Position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moverOptions.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_target.Position, transform.position) <= _moverOptions.AttackDistance) 
        {
            _attacker.Reload();
            _player.Stand();
            _isMoving = false;
        }
        else 
        {
            StartMove();
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

    private void StartMove()
    {
        if (_isMoving == false)
            _isMoving = true;
            Debug.Log("StartMove");

        if (_target == null)
        {
            Moved?.Invoke();
            _player.StartMove();
        }
    }
}