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

    public void MoveAim(Vector3 mousePosition)
    {
        _aimImage.transform.position = mousePosition;
    }

    public void MoveAim(Vector2 direction, Vector2 mousePosition)
    {
        if(_isHold)
            _derection = direction * _moveSpeedHold * Time.deltaTime;
        else
            _derection = mousePosition * _moveSpeed * Time.deltaTime;

        TryMoveAxisX(_derection.x);
        TryMoveAxisY(_derection.y);     
    }

    public void ShowAim()
    {
        _aimImage.gameObject.SetActive(true);
    }

    private void TryMoveAxisX(float x)
    {
        Vector2 position = _aimImage.transform.position;

        if (x < 0 && Position.x - _border < 0f)
        {
            _aimImage.transform.position = new Vector2(_border, position.y);
        }
        else if(x > 0 && Position.x + _border > _screenRect.width)
        {
            _aimImage.transform.position = new Vector2(_screenRect.width - _border, position.y);
        }
        else
        {
            _aimImage.transform.Translate(_derection);
        }
    }

    private void TryMoveAxisY(float y)
    {
        Vector2 position = _aimImage.transform.position;

        if (y < 0 && Position.y - _border < 0f)           
        {
            _aimImage.transform.position = new Vector2(position.x, _border);
        }
        else if (y > 0 && Position.y + _border > _screenRect.height)
        {
            _aimImage.transform.position = new Vector2(position.x, _screenRect.height - _border);
        }
        else
        {
            _aimImage.transform.Translate(_derection);
        }
    }
    private void DisableAim()
    {
        _aimImage.gameObject.SetActive(false);
    }
}