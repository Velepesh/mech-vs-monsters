using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(IDamageable))]
public class DamagedEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damagedEffect;
    [SerializeField] private List<Transform> _spawnPoints;

    private readonly int _firstPercent = 75;
    private readonly int _secondPercent = 50;
    private readonly int _thirdPercent = 25;

    private IDamageable _damageable;
    private Health _health;
    private int _previousPercent = 100;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
        _health = _damageable.Health;
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        if (_health.StartValue > 0)
        {
            int percent = GetPersent(health);

            if (percent <= _thirdPercent && _previousPercent > _thirdPercent)
                PlayEffect();

            if (percent <= _secondPercent && _previousPercent > _secondPercent)
                PlayEffect();

            if (percent <= _firstPercent && _previousPercent > _firstPercent)
                PlayEffect();

            _previousPercent = percent;
        }
    }

    private int GetPersent(int health)
    {
        return (health * 100) / _health.StartValue;
    }

    private void PlayEffect()
    {
        if (_spawnPoints.Count > 0)
        {
            Instantiate(_damagedEffect.gameObject, _spawnPoints[0]);
            _spawnPoints.Remove(_spawnPoints[0]);
        }
    }
}