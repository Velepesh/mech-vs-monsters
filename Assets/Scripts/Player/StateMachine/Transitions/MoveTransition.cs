using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(DownMover))]
[RequireComponent(typeof(AttackMover))]
public class MoveTransition : Transition
{
    [SerializeField] private DownMover _downMover;
    [SerializeField] private AttackMover _attackMover;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
        _downMover.Landed += OnLanded;
        _player.Moved += OnMoved;
        _attackMover.Moved += OnMoved;
    }

    private void OnDisable()
    {
        _downMover.Landed -= OnLanded;
        _player.Moved -= OnMoved;
        _attackMover.Moved -= OnMoved;
    }

    private void Transit()
    {
        NeedTransit = true;
        Debug.Log("NeedTransit" + NeedTransit);
    }

    private void OnLanded()
    {
        Transit();
    }

    private void OnMoved()
    {
        Transit();
    }
}