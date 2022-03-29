using UnityEngine;

public class RocketProjectile : DamageCollider
{
    [SerializeField] private int _damageOnHouse = 1000;
    [SerializeField] private int _damageOnBarricade = 150;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _explosionForce = 1;
    [SerializeField] private GameObject _rocketExplosion;
    [SerializeField] private ParticleSystem _disableOnHit;
    [SerializeField] private LayerMask _layerMask;

    private void OnValidate()
    {
        _damageOnHouse = Mathf.Clamp(_damageOnHouse, 0, int.MaxValue);
        _radius = Mathf.Clamp(_radius, 0, float.MaxValue);
        _explosionForce = Mathf.Clamp(_explosionForce, 0, float.MaxValue);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Explode();

        _disableOnHit.Stop();

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if (damageable is House)
                    DoDamage(damageable, _damageOnHouse);
                else if (damageable is Barricade)
                    DoDamage(damageable, _damageOnBarricade);
                else
                    DoDamage(damageable);

                if (collider.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.AddExplosionForce(_explosionForce, transform.position, _radius, 1f, ForceMode.Impulse);
            }
        }

        GameObject explosion = Instantiate(_rocketExplosion, transform.position, _rocketExplosion.transform.rotation, null);
    }
}