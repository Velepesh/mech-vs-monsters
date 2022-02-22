using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class AttackMover : MonoBehaviour
{
    [SerializeField] private StopDetector _stopDetector;
    [SerializeField] private TargetDetector _targetDetector;

    private Player _player;
    private PlayerMover _playerMover;
    private bool _isAttack = false;
    private ITarget _target;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
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
        //if (_playerMover.IsStopDistance)
        //{
        //    _isAttack = true;
        //    Debug.Log("IsStopDistance");
        //}
        //else
        //{
        //    Debug.Log("False");
        //    _isAttack = false;
        //}

        if (_playerMover.IsStopDistance && _player.IsDied == false)
        {
            Debug.Log("IsStopDistance");
            //if (_playerMover.IsStopDistance)
            //{
            _target = _targetDetector.GetClosestTarget(transform.position);
            if (_target != null)
            {
                _playerMover.LookAtTarget(_target.Position);

                
                Debug.Log("_isAttack == false");
                Debug.Log(Vector3.Distance(_target.Position, transform.position));
                if (Vector3.Distance(_target.Position, transform.position) > _playerMover.AttackDistance)
                {
                    Debug.Log("AttackDistance");
                    MoveToTargetPosition();
                }

                if (_isAttack)
                {
                    //    Debug.Log("_isAttack");
                    //    if (_target.IsDied)
                    //    {
                    //        _isAttack = false;
                    //        _player.StartMove();
                    //        Debug.Log("StartMove");
                    //    }
                    Debug.Log(" _player.Attack();");
                    if (_target.IsDied == false)
                    {
                        _player.Attack();         
                    }

                    if (_target.IsDied)
                    {
                        _isAttack = false;
                        _player.StartMove();
                        Debug.Log("StartMove");
                    }
                }
                else
                {
                    Debug.Log("FALSE");

                }
               
            }
            else
            {
               
                Debug.Log("NULL");
               // _player.StartMove();
            }
        }
        else
        {
            Debug.Log("(_playerMover.IsStopDistance = FALALAAL");
        }
    }

    private void MoveToTargetPosition()
    {
        //_player.StartMove();
        Vector3 targetPosition = new Vector3(_target.Position.x, transform.position.y, _target.Position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _playerMover.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_target.Position, transform.position) <= _playerMover.AttackDistance)
        {
            Debug.Log("_isAtta T R U E");
            _isAttack = true;
            _player.Stand();
        }
    }

    private void OnMovingStarted()
    {
       // StopAttack();
    }

    private void OnMovingStopped()
    {
        //StopAttack();
    }

    private void StopAttack()
    {
        _isAttack = false;
        //player.StartMove();
    }
}