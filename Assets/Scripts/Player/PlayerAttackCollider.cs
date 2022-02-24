using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackCollider : DamageCollider
{
    [SerializeField] private float _cooldownTime;

    private float _attackTimer;

    public event UnityAction<Vector3, Quaternion> Damaged;

    private bool _isAttack;

    private void Update()
    {
        if (_attackTimer > _cooldownTime)
        {
            _isAttack = true;
            _attackTimer = 0;
        }

        _attackTimer += Time.deltaTime;
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