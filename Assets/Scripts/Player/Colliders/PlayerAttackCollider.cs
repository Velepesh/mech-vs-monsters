using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackCollider : DamageCollider
{
    [SerializeField] private Attacker _attacker;


    public event UnityAction<Vector3, Quaternion> Damaged;

    private bool _isAttack;

    private void OnEnable()
    {
        _attacker.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _attacker.Attacked -= OnAttacked;
    }

    private void OnAttacked(float speed)
    {
        _isAttack = true;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (_isAttack)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 position = contact.point;

                Damaged?.Invoke(position, rotation);
                DoDamage(damageable);
                _isAttack = false;
            }
        }
    }
}