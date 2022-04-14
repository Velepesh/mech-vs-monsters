using UnityEngine;
using UnityEngine.UI;

public class GrenadeExplosionButton : MonoBehaviour
{
    [SerializeField] private GrenadeBullet _grenadeBullet;
    [SerializeField] private Button _button;

    private bool _isShow;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _grenadeBullet.TutorialShowed += OnTutorialShowed;
        _grenadeBullet.ButtonShowed += OnButtonShowed;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _grenadeBullet.TutorialShowed -= OnTutorialShowed;
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
            _grenadeBullet.EndTutorial();
    }

    private void OnButtonShowed()
    {
        if (_isShow == false)
        {
            ShowButton();
            _isShow = true;
        }
    }
    
    private void OnTutorialShowed()
    {
        _button.interactable = true;
    }

    private void ShowButton()
    {
        _button.gameObject.SetActive(true);

        if (_grenadeBullet.IsTutorial)
            _button.interactable = false;
    }

    private void DisableButton()
    {
        _button.gameObject.SetActive(false);
    }
}