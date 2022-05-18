using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigMonsterHealthView : MonoBehaviour
{
    [SerializeField] private List<TimerMonsterCollider> _timerMonsterColliders;
    [SerializeField] private Slider _slider;

    private float _currentValue;
    private float _maxValue;

    private void OnEnable()
    {
        for (int i = 0; i < _timerMonsterColliders.Count; i++)
            _maxValue += _timerMonsterColliders[i].Timer;

        _currentValue = _maxValue;
        _slider.maxValue = _maxValue;
        _slider.value = _maxValue;
    }

    private void Update()
    {
        _currentValue = 0f;

        for (int i = 0; i < _timerMonsterColliders.Count; i++)
            _currentValue += _timerMonsterColliders[i].Timer;

        _slider.value = _currentValue;

        if (_currentValue <= 0f)
            DisableSlider();
    }

    private void DisableSlider()
    {
        _slider.gameObject.SetActive(false);
    }
}