using UnityEngine;
using UnityEngine.UI;

public class GrenadeExplosionButton : MonoBehaviour
{
    [SerializeField] private GrenadeBullet _grenadeBullet;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _grenadeBullet.ButtonShowed += OnButtonShowed;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _grenadeBullet.ButtonShowed -= OnButtonShowed;
    }

    private void Start()
    {
        DisableButton();
    }

    private void OnButtonClick()
    {
        _grenadeBullet.Explosion();

        if (_grenadeBullet.IsTutorial)
            _grenadeBullet.StartGrenade();
    }

    private void OnButtonShowed()
    {
        ShowButton();
    }

    private void ShowButton()
    {
        _button.gameObject.SetActive(true);
    }

    private void DisableButton()
    {
        _button.gameObject.SetActive(false);
    }
}