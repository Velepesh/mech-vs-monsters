using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IDamageable))]
public class HealthView : MonoBehaviour
{
   [SerializeField] private float _duration;
    [SerializeField] private float _startDelayTime;
    [SerializeField] private Game _game;
    [SerializeField] private Slider _slider;
   
    private IDamageable _damageable;
    private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
        _slider.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _damageable.Health.HealthChanged += OnHealthChanged;
        _game.LevelStarted += OnLevelStarted;

        if(_damageable is Player player)
            player.Fought += OnFought;
    }

    private void OnDisable()
    {
        _damageable.Health.HealthChanged -= OnHealthChanged;
        _game.LevelStarted -= OnLevelStarted;

        if (_damageable is Player player)
            player.Fought -= OnFought;
    }

    private void OnFought(Monster monster)
    {
        OnLevelStarted();
    }

    private void OnLevelStarted()
    {
        _maxHealth = _damageable.Health.Value;
        _currentHealth = _damageable.Health.Value;
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;

        StartCoroutine(EnableSlider(_startDelayTime));
    }

    private void OnHealthChanged(int health)
    {
        _currentHealth = health;
    }

    private void Update()
    {
        if (_slider.value != _currentHealth)
            _slider.value = Mathf.Lerp(_slider.value, _currentHealth, _duration * Time.deltaTime);

        if (_currentHealth == 0)
            DisableSlider();
    }

    private IEnumerator EnableSlider(float duration)
    {
        yield return new WaitForSeconds(duration);

        _slider.gameObject.SetActive(true);
    }

    private void DisableSlider()
    {
        _slider.gameObject.SetActive(false);
    }
}