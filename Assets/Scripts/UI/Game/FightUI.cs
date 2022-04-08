using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _startDelayTime;
    [SerializeField] private Game _game;
    [SerializeField] private Slider _playerSlider;
    [SerializeField] private Slider _monsterSlider;
    [SerializeField] private Player _player;
    [SerializeField] private Monster _monster;

    private int _currentPlayerHealth;
    private int _currentGodzillaHealth;

    private void Awake()
    {
        DisableAllSliders();
    }

    private void OnEnable()
    {
        _player.Health.HealthChanged += OnPlayerHealthChanged;
        _monster.Health.HealthChanged += OnGodzillaHealthChanged;
        _game.Fought += OnFought;
        _game.FoughtWon += OnFoughtWon;
    }

    private void OnDisable()
    {
        _player.Health.HealthChanged -= OnPlayerHealthChanged;
        _monster.Health.HealthChanged -= OnGodzillaHealthChanged;
        _game.Fought -= OnFought;
        _game.FoughtWon -= OnFoughtWon;
    }

    private void OnFought()
    {
        InitSliderValues(_playerSlider, _player.Health.StartValue, out _currentPlayerHealth);
        InitSliderValues(_monsterSlider, _monster.Health.StartValue, out _currentGodzillaHealth);

        StartCoroutine(EnableAllSliders(_startDelayTime));
    }

    private void InitSliderValues(Slider slider, int startHealth, out int currentHealth)
    {
        currentHealth = startHealth;

        slider.maxValue = currentHealth;
        slider.value = currentHealth;
    }

    private void OnFoughtWon()
    {
        DisableAllSliders();
    }

    private void OnPlayerHealthChanged(int health)
    {
        _currentPlayerHealth = health;
    }

    private void OnGodzillaHealthChanged(int health)
    {
        _currentGodzillaHealth = health;
    }

    private void Update()
    {
        TryToChangeSliderValue(_playerSlider, _currentPlayerHealth);
        
        TryToChangeSliderValue(_monsterSlider, _currentGodzillaHealth);

        if (_currentPlayerHealth == 0 || _currentGodzillaHealth == 0)
            DisableAllSliders();
    }

    private void TryToChangeSliderValue(Slider slider, int currentHealth)
    {
        if (slider.value != currentHealth)
            slider.value = Mathf.Lerp(slider.value, currentHealth, _speed * Time.deltaTime);
    }

    private IEnumerator EnableAllSliders(float duration)
    {
        yield return new WaitForSeconds(duration);
        EnableSlider(_playerSlider);
        EnableSlider(_monsterSlider);
    }

    private void EnableSlider(Slider slider)
    {
        slider.gameObject.SetActive(true);
    }

    private void DisableAllSliders()
    {
        DisableSlider(_playerSlider);
        DisableSlider(_monsterSlider);
    }

    private void DisableSlider(Slider slider)
    {
        slider.gameObject.SetActive(false);
    }
}