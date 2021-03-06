using UnityEngine;
using TMPro;

public class MenuHealthView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _healthText;

    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _player.Health.Value;

        _healthText.text = _currentHealth.ToString();

        _player.Health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.Health.HealthChanged -= OnHealthChanged;
    }
    
    private void OnHealthChanged(int health)
    {
        _healthText.text = health.ToString();
    }
}