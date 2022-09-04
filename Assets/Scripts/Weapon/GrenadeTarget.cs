using UnityEngine;
using UnityEngine.UI;

public class GrenadeTarget : MonoBehaviour
{
    [SerializeField] private GrenadeBullet _grenadeBullet;
    [SerializeField] private Image _image;
    [SerializeField] private Collider _collider;

    private bool _isShow;

    private void OnEnable()
    {
        _grenadeBullet.TutorialShowed += OnTutorialShowed;
        _grenadeBullet.ButtonShowed += OnButtonShowed;
    }

    private void OnDisable()
    {
        _grenadeBullet.TutorialShowed -= OnTutorialShowed;
        _grenadeBullet.ButtonShowed -= OnButtonShowed;
    }

    private void Start()
    {
        DisableButton();
    }

    private void OnButtonShowed()
    {
        if (_isShow == false)
        {
            ShowTarget();
            _isShow = true;
        }
    }

    private void OnTutorialShowed()
    {
        _collider.enabled = true;
    }

    private void ShowTarget()
    {
        _image.gameObject.SetActive(true);

        if (_grenadeBullet.IsTutorial)
            _collider.enabled = false;
    }

    private void DisableButton()
    {
        _image.gameObject.SetActive(false);
    }
}