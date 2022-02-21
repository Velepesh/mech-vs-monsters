using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RocketLauncherButton : MonoBehaviour
{
    [SerializeField] private RocketLauncher _rocketLauncher;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

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
        _rocketLauncher.TryShoot();
    }
}