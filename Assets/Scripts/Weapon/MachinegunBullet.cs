using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MachinegunBullet : DamageCollider
{
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private float _destroyTime = 1.2f;
    
    private float _timer;
    private Rigidbody _rigidbody;

    private void OnValidate()
    {
        _destroyTime = Mathf.Clamp(_destroyTime, 0f, float.MaxValue);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private  void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _destroyTime)
            Destroy();
    }

    private void FixedUpdate()
    {
        if (_timer >= 0.05f)
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            DoDamage(damageable);

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);

        Vector3 pos = contact.point;
        Instantiate(_impactPrefab, pos, rot);

        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}