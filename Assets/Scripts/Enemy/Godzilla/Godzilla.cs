using UnityEngine;
using UnityEngine.Events;

public class Godzilla : Enemy, IDamageable, IDieingPolicy, ITarget
{
    [SerializeField] private float _attackTime;
    [SerializeField] private GameObject _model;

    private int _startHealth;

    public int StartHealth => _startHealth;

    public event UnityAction Won;
    public event UnityAction AttackStarted;
    public event UnityAction Attacked;
    public event UnityAction AttackStopped;

    private void OnValidate()
    {
        _attackTime = Mathf.Clamp(_attackTime, 0, float.MaxValue);
    }

    private void Start()
    {
        _startHealth = Health;
        DisableModel();;
    }

    public void Fight()
    {
        EnableModel();
        AttackStarted?.Invoke();
    }

    public void Attack()
    {
        Attacked?.Invoke();
    }

    public void Win()
    {
        Won?.Invoke();
    }

    public void StopAttack()
    {
        AttackStopped?.Invoke();
    }

    private void EnableModel()
    {
        _model.SetActive(true);
    }

    private void DisableModel()
    {
        _model.SetActive(false);
    }
}