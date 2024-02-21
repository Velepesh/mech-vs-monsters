using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherButton : MonoBehaviour
{
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _rocketLauncher.Shoot();
    }
}