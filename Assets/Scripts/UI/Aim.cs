using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Image _aimImage;
    [SerializeField] private float _moveSpeedHold = 400;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _border;
    [SerializeField] private bool _isHold;

    private Vector2 _derection;
    private Rect _screenRect;
    public Vector2 Position => _aimImage.transform.position;
   
    private void Start()
    {
        _screenRect = new Rect(0, 0, Screen.width, Screen.height);
        DisableAim();
    }
    public void MoveAim(Vector2 direction, Vector2 mousePosition)
    {
        if(_isHold)
            _derection = direction * _moveSpeedHold * Time.deltaTime;
        else
            _derection = mousePosition * _moveSpeed * Time.deltaTime;

        TryMoveAxisX(_derection.x);
        TryMoveAxisY(_derection.y);

        _aimImage.transform.Translate(_derection);
    }

    public void ShowAim()
    {
        _aimImage.gameObject.SetActive(true);
    }

    private void TryMoveAxisX(float x)
    {
        if (x < 0 && Position.x - _border < 0f
            || x > 0 && Position.x + _border > _screenRect.width)
        {
            _derection = Vector3.zero;
        }
    }

    private void TryMoveAxisY(float y)
    {
        if (y < 0 && Position.y - _border < 0f
            || y > 0 && Position.y + _border > _screenRect.height)
        {
            _derection = Vector3.zero;
        }
    }
    private void DisableAim()
    {
        _aimImage.gameObject.SetActive(false);
    }
}