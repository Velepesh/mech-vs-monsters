using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackCollider : DamageCollider
{
    public event UnityAction<Vector3, Quaternion> Damaged;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;

            Damaged?.Invoke(position, rotation);
            DoDamage(damageable);
        }
    }
}