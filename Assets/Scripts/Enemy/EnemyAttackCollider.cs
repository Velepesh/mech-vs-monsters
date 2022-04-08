using UnityEngine;

public class EnemyAttackCollider : DamageCollider
{
    [SerializeField] private ParticleSystem _hitEffect;

    protected override void OnCollisionEnter(Collision collision)
    {    
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 position = contact.point;
            Instantiate(_hitEffect.gameObject, position, rotation);
          
            DoDamage(damageable);
        }     
    }
}