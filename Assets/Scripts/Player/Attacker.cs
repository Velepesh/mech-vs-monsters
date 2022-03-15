using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _defaulttAnimatorSpeed = 1.15f;
    [SerializeField] private float _fightAnimatorSpeed = 1f;
    [SerializeField] private float _cooldownTime;

    private float _shootingTimer;

    public event UnityAction<float> Attacked;

    private void OnValidate()
    {
        _defaulttAnimatorSpeed = Mathf.Clamp(_defaulttAnimatorSpeed, 0f, float.MaxValue);
        _fightAnimatorSpeed = Mathf.Clamp(_fightAnimatorSpeed, 0f, float.MaxValue);
        _cooldownTime = Mathf.Clamp(_cooldownTime, 0f, float.MaxValue);
    }
    
    private void Update()
    {
        _shootingTimer += Time.deltaTime;
    }

    public void Attack(bool isFight)
    {
        if (_shootingTimer >= _cooldownTime)
        {
            if(isFight)
                Attacked?.Invoke(1f);
            else
                Attacked?.Invoke(_defaulttAnimatorSpeed);

            _shootingTimer = 0;
        }
    }

    public void Reload()
    {
        _shootingTimer = _cooldownTime;
    }
}