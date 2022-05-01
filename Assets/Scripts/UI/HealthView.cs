using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IDamageable))]
abstract public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _speed;

    private IDamageable _damageable;
    private int _currentHealth;

    protected void Init(IDamageable damageable)
    {
        AssignStartValues(damageable);
    }

    protected void EnableSlider()
    {
        _slider.gameObject.SetActive(true);
    }

    protected void DisableSlider()
    {
        _slider.gameObject.SetActive(false);
    }

    protected void ChangeSliderValue()
    {
        if (_slider.value != _currentHealth)
            _slider.value = Mathf.Lerp(_slider.value, _currentHealth, _speed * Time.deltaTime);

        if (_currentHealth == 0)
            DisableSlider();
    }

    protected void ApplyHealth(int health)
    {
        _currentHealth = health;
    }

    private void Update()
    {
        ChangeSliderValue();
    }

    private void AssignStartValues(IDamageable damageable)
    {
        _currentHealth = damageable.Health.Value;
        _slider.maxValue = _currentHealth;
        _slider.value = _currentHealth;
    }
}