using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class FighterMover : MonoBehaviour
{
    private Player _player;
    private PlayerMover _playerMover;
    private Vector3 _godzillaPosition;
    private Vector3 _targetFightPosition;
    private bool _isFight = false;
  
    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
        _player.Prepeared += OnPrepeared;
        _player.MovingStarted += OnMovingStarted;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.Prepeared -= OnPrepeared;
        _player.MovingStarted -= OnMovingStarted;
    }

    private void Update()
    {
        if (_isFight)
           MoveToTarget();
    }

    private void MoveToTarget()
    {
        _playerMover.LookAtTarget(_godzillaPosition);
        transform.position = Vector3.MoveTowards(transform.position, _targetFightPosition, _playerMover.MoveSpeed * Time.deltaTime);
    }

    private void OnPrepeared(Transform targetPoint, Godzilla godzilla)
    {
        _godzillaPosition = godzilla.Position;
        _targetFightPosition = targetPoint.position;
        _targetFightPosition.y = transform.position.y;

        _isFight = true;
    }

    private void OnWon()
    {
        EndFight();
    }

    private void OnMovingStarted()
    {
        EndFight();
    }

    private void EndFight()
    {
        _isFight = false;
    }
}