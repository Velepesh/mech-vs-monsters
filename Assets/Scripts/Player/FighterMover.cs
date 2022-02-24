using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMover))]
public class FighterMover : MonoBehaviour
{
    [SerializeField] private float _moveToTargetPointTime;

    private Player _player;
    private PlayerMover _playerMover;
    private Vector3 _godzillaPosition;
    private Vector3 _targetFightPosition;
    private bool _isFight = false;

    private void OnValidate()
    {
        _moveToTargetPointTime = Mathf.Clamp(_moveToTargetPointTime, 0f, float.MaxValue);
    }
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
            _playerMover.LookAtTarget(_godzillaPosition);
    }

    //private IEnumerator MoveToTarget()
    //{
    //   // _playerMover.LookAtTarget(_godzillaPosition);
    //    transform.position = Vector3.MoveTowards(transform.position, _targetFightPosition, _playerMover.MoveSpeed * Time.deltaTime);
    //}


    private IEnumerator Move(float duration)
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, _targetFightPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnPrepeared(Transform targetPoint, Godzilla godzilla)
    {
        _godzillaPosition = godzilla.Position;
        _targetFightPosition = targetPoint.position;
        _targetFightPosition.y = transform.position.y;

        _isFight = true;
        StartCoroutine(Move(_moveToTargetPointTime));
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