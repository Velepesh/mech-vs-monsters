using UnityEngine;

[RequireComponent(typeof(Player))]
public class DieTransition : Transition
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
    }

    private void OnDied(IDamageable damageable)
    {
        NeedTransit = true;
    }
}