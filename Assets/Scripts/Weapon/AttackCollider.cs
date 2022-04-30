using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AttackCollider : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage => _damage;

    private void OnValidate()
    {
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
    }

    protected abstract void OnCollisionEnter(Collision collision);

    protected void Attack(IDamageable damageable)
    { 
        damageable.TakeDamage(_damage);
    }

    protected void Attack(IDamageable damageable, int damage)
    {
        damageable.TakeDamage(damage);
    }
}