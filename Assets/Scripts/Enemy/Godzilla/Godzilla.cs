using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Godzilla : Enemy, IDamageable, IDieingPolicy, ITarget
{
    [SerializeField] private float _attackTime;
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _healthBar;

    private Collider _collider;

    public event UnityAction Won;
    public event UnityAction Attacked;
    public event UnityAction AttackStopped;

    private void OnValidate()
    {
        _attackTime = Mathf.Clamp(_attackTime, 0, float.MaxValue);
    }

    private void Start()
    { 
        _collider = GetComponent<Collider>();
        DisableModel();;
    }

    public void Fight()
    {
        EnableModel();
        Attacked?.Invoke();
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
        _healthBar.SetActive(true);
        _collider.enabled = true;
        _model.SetActive(true);
    }

    private void DisableModel()
    {
        _healthBar.SetActive(false);
        _collider.enabled = false;
        _model.SetActive(false);
    }
}