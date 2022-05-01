using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(DownMover))]
public class PlayerHealthView : HealthView
{
    private Player _player;
    private DownMover _downMover;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _downMover = GetComponent<DownMover>();
    }

    private void OnEnable()
    {
        _player.Fought += OnFought;
        _downMover.Landed += OnLanded;
        _player.Health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.Fought -= OnFought;
        _downMover.Landed -= OnLanded;
        _player.Health.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        DisableSlider();
    }

    private void OnFought(Monster monster)
    {
        OnLanded();
    }

    private void OnLanded()
    {
        if (_player is IDamageable damageable)
            Init(damageable);

        EnableSlider();
    }

    private void OnHealthChanged(int health)
    {
        ApplyHealth(health);
    }
}