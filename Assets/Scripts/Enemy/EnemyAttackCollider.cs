using UnityEngine;

public class EnemyAttackCollider : AttackCollider
{
    [SerializeField] private ParticleSystem _hitEffect;
    
    private readonly float _cooldownTime = 0.5f;
    
    private float _shootingTimer;
    private bool _canAttack;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (_canAttack)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 position = contact.point;
                Instantiate(_hitEffect.gameObject, position, rotation);

                Attack(damageable);
                _canAttack = false;
            }
        }     
    }

    private void Update()
    {
        if (_shootingTimer >= _cooldownTime)
        {
            _canAttack = true;
            _shootingTimer = 0;
        }

        _shootingTimer += Time.deltaTime;
    }
}