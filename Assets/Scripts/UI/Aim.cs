using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Image _aimImage;
    [SerializeField] private float _moveSpeedTouch = 60;
    [SerializeField] private float _border;

    private Vector2 _derection;
    private Rect _screenRect;
    public Vector2 Position => _aimImage.transform.position;

    private void Start()
    {
        _screenRect = new Rect(0, 0, Screen.width, Screen.height);
        DisableAim();
    }

    public void MoveAim(Vector2 mousePosition)
    {
        _derection = mousePosition * _moveSpeedTouch;
      
        _aimImage.transform.Translate(_derection * Time.deltaTime);
    }

    private void LateUpdate()
    {
        _aimImage.transform.position = new Vector2(Mathf.Clamp(_aimImage.transform.position.x, _border, _screenRect.width - _border),
            Mathf.Clamp(_aimImage.transform.position.y, _border, _screenRect.height - _border));
    }

    public void ShowAim()
    {
        _aimImage.gameObject.SetActive(true);
    }

    public void DisableAim()
    {
        _aimImage.gameObject.SetActive(false);
    }
}