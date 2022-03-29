using UnityEngine;

public class DownMoverTransition : Transition
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.Fell += OnFell;
    }

    private void OnDisable()
    {
        _player.Fell -= OnFell;
    }

    private void OnFell()
    {
        NeedTransit = true;
    }
}