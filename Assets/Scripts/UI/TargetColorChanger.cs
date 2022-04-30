using UnityEngine;
using UnityEngine.UI;

public class TargetColorChanger : MonoBehaviour
{
    [SerializeField] private TimerMonsterCollider _timerMonsterCollider;
    [SerializeField] private Image _targetImage;
    [SerializeField] private Color _hittedColor;
    [SerializeField] private Color _defaultColor;

    private void OnEnable()
    {
        _timerMonsterCollider.DefaultColorSelected += OnDefaultColorSelected;
        _timerMonsterCollider.HittedColorSelected += OnHittedColorSelected;
    }

    private void OnDisable()
    {
        _timerMonsterCollider.DefaultColorSelected -= OnDefaultColorSelected;
        _timerMonsterCollider.HittedColorSelected -= OnHittedColorSelected;
    }

    private void Start()
    {
        OnDefaultColorSelected();
    }

    private void OnDefaultColorSelected()
    {
        _targetImage.color = _defaultColor;
    }

    private void OnHittedColorSelected()
    {
        _targetImage.color = _hittedColor;
    }
}