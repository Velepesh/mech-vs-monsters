using UnityEngine;
using TMPro;

public class GearView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GearsCollector _gearsCollector;

    private int _points;

    private readonly int _pointsMultiplier = 100;

    private void OnEnable()
    {
        _gearsCollector.GearsNumberChanged += OnGearsNumberChanged;
        ChangeText(_points);
    }

    private void OnDisable()
    {
        _gearsCollector.GearsNumberChanged -= OnGearsNumberChanged;
    }

    private void OnGearsNumberChanged(int number)
    {
        _points += number * _pointsMultiplier;
        ChangeText(_points);
    }

    private void ChangeText(int points)
    {
        _text.text = _points.ToString();
    }
}