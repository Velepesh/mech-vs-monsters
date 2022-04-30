using UnityEngine;
using UnityEngine.Events;

public class TimerMonsterCollider : MonoBehaviour
{
    [SerializeField] private float _timer;

    private bool _isTimer;

    public float Timer => _timer;

    public event UnityAction<TimerMonsterCollider> TimerEnded;
    public event UnityAction DefaultColorSelected;
    public event UnityAction HittedColorSelected;

    private void Update()
    {
        if (_isTimer)
        {
            _timer -= Time.deltaTime;
          
            if (_timer <= 0f)
                TimerEnded?.Invoke(this);
        }
    }

    public void TakeDamage()
    {
        if(_isTimer == false)
        {
            _isTimer = true;
            HittedColorSelected?.Invoke();
        }
    }

    public void SwitchToDefaultState()
    {
        _isTimer = false;
        DefaultColorSelected?.Invoke();
    }
}