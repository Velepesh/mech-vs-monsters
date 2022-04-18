using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackCollider : DamageCollider
{
    [SerializeField] private Attacker _attacker;

    private readonly float _cooldownTime = 0.5f;

    private float _shootingTimer;
    private bool _isAttack;
    private bool _canAttack;

    public event UnityAction<Vector3, Quaternion> Damaged;

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
        if (_isAttack && _canAttack)
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