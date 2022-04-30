using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimerMonsterCollider : MonoBehaviour
{
    [SerializeField] private float _timer;
    //[SerializeField] private Image _targetImage;
    //[SerializeField] private Color _hittedColor;
    //[SerializeField] private Color _startColor;

    private bool _isTimer;
   

    public event UnityAction<TimerMonsterCollider> TimerEnded;

    private void Start()
    {
        //ChangeColor(_startColor);
    }

    private void Update()
    {
        if (_isTimer)
        {
            //if(_targetImage.color != _hittedColor)
            //    ChangeColor(_hittedColor);

            _timer -= Time.deltaTime;
          
            if (_timer <= 0f)
                TimerEnded?.Invoke(this);
        }
    }

    public void TakeDamage()
    {
        _isTimer = true;
    }

    public void SwitchToDefaultState()
    {
        _isTimer = false;
       // ChangeColor(_startColor);
    }

    private void ChangeColor(Color color)
    {
        //_targetImage.color = color;
    }
}