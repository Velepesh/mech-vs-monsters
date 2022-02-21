using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MachinegunBullet : DamageCollider
{
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private GameObject _explosionPrefab;   
    [SerializeField] private bool _lookRotation = true;
    [SerializeField] private bool _ignorePrevRotation = false;
    [SerializeField] private bool _explodeOnTimer = false;
    [SerializeField] private float _explosionTimer;
    [SerializeField] private float _destroyTime = 1.2f;
    
    private float _timer;
    private Rigidbody _rigidbody;

    private void OnValidate()
    {
        _explosionTimer = Mathf.Clamp(_explosionTimer, 0f, float.MaxValue);
        _destroyTime = Mathf.Clamp(_destroyTime, 0f, float.MaxValue);
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private  void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _explosionTimer && _explodeOnTimer == true)
            Explode();

        if(_timer >= _destroyTime)
            Destroy();
    }

    private void FixedUpdate()
    {
        if (_lookRotation && _timer >= 0.05f)
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            DoDamage(damageable);

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);

        if (_ignorePrevRotation)
            rot = Quaternion.Euler(0, 0, 0);

        Vector3 pos = contact.point;
        Instantiate(_impactPrefab, pos, rot);

        if (_explodeOnTimer == false)
            Destroy(gameObject);
    }

    private void Explode()
    {
        Instantiate(_explosionPrefab, gameObject.transform.position, Quaternion.identity);
        Destroy();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}