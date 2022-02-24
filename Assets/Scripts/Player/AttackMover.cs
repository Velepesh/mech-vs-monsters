using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class AttackMover : MonoBehaviour
{
    [SerializeField] private StopDetector _stopDetector;
    [SerializeField] private TargetDetector _targetDetector;

    private Player _player;
    private PlayerMover _playerMover;
    private bool _canAttack = true;
    private ITarget _target;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
        _player.MovingStarted += OnMovingStarted;
        _player.MovingStopped += OnMovingStopped;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.MovingStarted -= OnMovingStarted;
        _player.MovingStopped -= OnMovingStopped;
    }

    private void Update()
    {
        if (_canAttack)
        {
            if (_playerMover.IsStopDistance)
            {
                if (_player.IsDied == false)
                {
                    Debug.Log("_playerMover.IsStopDistance");
                    _target = _targetDetector.GetClosestTarget(transform.position);

                    if (_target != null)
                    {
                        Debug.Log("_target != null");
                        _playerMover.LookAtTarget(_target.Position);

                        if (Vector3.Distance(_target.Position, transform.position) > _playerMover.AttackDistance)
                        {
                            Debug.Log("or3.Distance(_target");
                            Vector3 targetPosition = new Vector3(_target.Position.x, transform.position.y, _target.Position.z);
                            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _playerMover.MoveSpeed * Time.deltaTime);

                            if (Vector3.Distance(_target.Position, transform.position) <= _playerMover.AttackDistance)
                                _player.Stand();
                        }
                        else
                        {
                            Debug.Log("<<<<=====");
                            if (_target == null)
                                _player.StartMove();
                            else
                                _player.Attack();
                        }
                    }
                    else
                    {
                        Debug.Log("target NUUUULLLL");
                        _player.StartMove();
                    }
                }
            }
            else
            {
                Debug.Log("NOT NOT IsStopDistance");
            }
        }
        //else
        //{
            //_player.StartMove();
            // _isAttack = false;
       // }
       // }
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

        //if (_playerMover.IsStopDistance && _player.IsDied == false)
        //{
        //    Debug.Log("IsStopDistance");
        //    //if (_playerMover.IsStopDistance)
        //    //{
        //    _target = _targetDetector.GetClosestTarget(transform.position);
        //    if (_target != null)
        //    {
        //        _playerMover.LookAtTarget(_target.Position);

                
        //        Debug.Log("_isAttack == false");
        //        Debug.Log(Vector3.Distance(_target.Position, transform.position));
        //        if (Vector3.Distance(_target.Position, transform.position) > _playerMover.AttackDistance)
        //        {
        //            Debug.Log("AttackDistance");
        //            MoveToTargetPosition();
        //        }

        //        if (_isAttack)
        //        {
        //            //    Debug.Log("_isAttack");
        //            //    if (_target.IsDied)
        //            //    {
        //            //        _isAttack = false;
        //            //        _player.StartMove();
        //            //        Debug.Log("StartMove");
        //            //    }
        //            Debug.Log(" _player.Attack();");
        //            if (_target.IsDied == false)
        //            {
        //                _player.Attack();         
        //            }

        //            if (_target.IsDied)
        //            {
        //                _isAttack = false;
        //                _player.StartMove();
        //                Debug.Log("StartMove");
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log("FALSE");

        //        }
               
        //    }
        //    else
        //    {
               
        //        Debug.Log("NULL");
        //       // _player.StartMove();
        //    }
        //}
        //else
        //{
        //    Debug.Log("(_playerMover.IsStopDistance = FALALAAL");
        //}
    }

 
    private void MoveToTargetPosition()
    {
        //_player.StartMove();
        Vector3 targetPosition = new Vector3(_target.Position.x, transform.position.y, _target.Position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _playerMover.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_target.Position, transform.position) <= _playerMover.AttackDistance)
        {
            Debug.Log("_isAtta T R U E");
            _canAttack = true;
            _player.Stand();
        }
    }

    private void OnMovingStarted()
    {
        //StopAttack();
    }

    private void OnMovingStopped()
    {
       // StopAttack();
    }

    private void StopAttack()
    {
        _canAttack = false;
        //player.StartMove();
    }
    
    private void OnWon()
    {
        StopAttack();
        //player.StartMove();
    }
}