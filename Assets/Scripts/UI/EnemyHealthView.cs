using UnityEngine;

[RequireComponent(typeof(IDamageable))]
public class EnemyHealthView : HealthView
{
    private IDamageable _damageable;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        _damageable.Health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _damageable.Health.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        EnableSlider();
        Init(_damageable);
    }

    private void OnHealthChanged(int health)
    {
        ApplyHealth(health);
    }
}