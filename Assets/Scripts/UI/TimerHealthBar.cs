using UnityEngine;
using UnityEngine.UI;

public class TimerHealthBar : MonoBehaviour
{
    [SerializeField] private TimerMonsterCollider _timerMonsterCollider;
    [SerializeField] private Slider _slider;

    private float _maxValue;

    private void Awake()
    {
        _slider.gameObject.SetActive(false);
    }

    private void Start()
    {
        _maxValue = _timerMonsterCollider.Timer;
        _slider.maxValue = _maxValue;
        _slider.value = _maxValue;

        _slider.gameObject.SetActive(true);
    }

    private void Update()
    {
        _slider.value = _timerMonsterCollider.Timer;

        if (_timerMonsterCollider.Timer <= 0f)
            DisableSlider();
    }

    private void DisableSlider()
    {
        _slider.gameObject.SetActive(false);
    }
}