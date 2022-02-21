using UnityEngine;
using TMPro;

public class MenuHealthView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _healthText;

    private int _currentMoney;

    private void OnEnable()
    {
        _currentMoney = _player.Health;

        _healthText.text = _currentMoney.ToString();

        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }
    
    private void OnHealthChanged(int health)
    {
        _healthText.text = health.ToString();
    }
}