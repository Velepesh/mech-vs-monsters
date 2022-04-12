using UnityEngine;

public class RocketProjectile : DamageCollider, IMover
{
    [SerializeField] private int _maxDamage = 500;
    [SerializeField] private int _monsterDamage = 150;
    [SerializeField] private float _radius = 1;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _rocketExplosion;
    [SerializeField] private LayerMask _layerMask;

    private ITarget _target;
    private Vector3 _lastPosition;

    private void OnValidate()
    {
        _maxDamage = Mathf.Clamp(_maxDamage, 0, int.MaxValue);
        _speed = Mathf.Clamp(_speed, 0, float.MaxValue);
        _radius = Mathf.Clamp(_radius, 0, float.MaxValue);
    }

    public void Init(ITarget target)
    {
        _target = target;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (_target != null)
        {
            if (_target.IsDied == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.Position, _speed * Time.deltaTime);
                _lastPosition = (_target.Position - transform.position).normalized;
            }
            else if (_target.IsDied)
            {
                if (_lastPosition == Vector3.zero)
                    _lastPosition = Vector3.forward;

                transform.Translate(_lastPosition * _speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _layerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out IDamageable damageable))
            {
                if (damageable is MonsterCollider)
                    DoDamage(damageable, _monsterDamage);
                else if(damageable is Vehicle == false)
                    DoDamage(damageable, _maxDamage);
                else
                    DoDamage(damageable);
                Debug.Log(collider.gameObject.name);
            }
        }
        GameObject explosion = Instantiate(_rocketExplosion, transform.position, _rocketExplosion.transform.rotation, null);
       
        Destroy(gameObject);
    }

    public void LookAtTarget(Vector3 target)
    {
        throw new System.NotImplementedException();
    }
}