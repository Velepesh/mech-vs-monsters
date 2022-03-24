using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(DownMover))]
public class MoveTransition : Transition
{
    [SerializeField] private DownMover _downMover;

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
    }

    private void OnDisable()
    {
        _downMover.Landed -= OnLanded;
        _player.Moved -= OnMoved;
    }

    private void Transit()
    {
        NeedTransit = true;
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