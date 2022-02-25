using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class AttackMover : MonoBehaviour
{
    [SerializeField] private StopDetector _stopDetector;
    [SerializeField] private TargetDetector _targetDetector;
    [SerializeField] private float _cooldownTime;

    private float _shootingTimer;
    private Player _player;
    private PlayerMover _playerMover;
    private bool _canAttack = true;
    private bool _isMoving = true;
    private ITarget _target;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    private void Update()
    {
        if (_canAttack)
        {
            if (_playerMover.IsStopDistance && _player.IsDied == false)
            {
                _target = _targetDetector.GetClosestTarget(transform.position);

                if (_target != null)
                {
                    _playerMover.LookAtTarget(_target.Position);

                    if (Vector3.Distance(_target.Position, transform.position) > _playerMover.AttackDistance)
                    {
                        MoveToTargetPosition();
                    }
                    else
                    {
                        //if (_target == null)
                        //{
                        //   // _player.StartMove();
                        //}
                       // else 
                        if (_shootingTimer >= _cooldownTime)
                        {
                            Debug.Log("ATTACK");
                            _player.Attack();
                            _shootingTimer = 0;
                        }
                       
                        Debug.Log("RELOAD");

                        if (_target == null)
                            _shootingTimer = _cooldownTime; 
                    }
                }
                else
                {
                    _player.StartMove();
                }
            }

        }
        _shootingTimer += Time.deltaTime;
    }

 
    private void MoveToTargetPosition()
    {
        Vector3 targetPosition = new Vector3(_target.Position.x, transform.position.y, _target.Position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _playerMover.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_target.Position, transform.position) <= _playerMover.AttackDistance) 
        {
            _shootingTimer = _cooldownTime;
            _player.Stand();
            _isMoving = false;
        }
        else if(_isMoving == false)
        {
            _player.StartMove();
            _isMoving = true;
        }
    }

    private void StopAttack()
    {
        _canAttack = false;
    }
    
    private void OnWon()
    {
        StopAttack();
    }
}