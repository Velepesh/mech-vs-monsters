using UnityEngine;
using TMPro;

public class MenuSpeedView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _speedText;

    private float _currentSpeed;

    private void OnEnable()
    {
         _currentSpeed = _player.Speed;

        _speedText.text = _currentSpeed.ToString();

        _player.SpeedChanged += OnSpeedChanged;
    }

    private void OnDisable()
    {
        _player.SpeedChanged -= OnSpeedChanged;
    }

    private void OnSpeedChanged(int speed)
    {
        _speedText.text = speed.ToString();
    }
}