using UnityEngine;

[RequireComponent(typeof(Player))]
public class FightTransition : Transition
{
    [SerializeField] private FighterMover _fighterMover;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
        _player.Prepeared += OnPrepeared;
    }

    private void OnDisable()
    {
        _player.Prepeared -= OnPrepeared;
    }

    private void Transit()
    {
        NeedTransit = true;
    }

    private void OnPrepeared(Transform transform, Monster monster, bool isAimnig)
    {
        Transit();
        _fighterMover.Init(transform, monster);
    }
}