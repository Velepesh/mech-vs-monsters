using UnityEngine;

[RequireComponent(typeof(Player))]
public class WinTransition : Transition
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    private void OnWon()
    {
        NeedTransit = true;
    }
}
