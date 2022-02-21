using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Godzilla : Enemy, IDamageable, IDieingPolicy, ITarget
{
    [SerializeField] private float _attackTime;

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
        _collider.enabled = false;
    }

    public void Fight()
    {
        _collider.enabled = true;
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
}