using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private Soldier _soldier;
    [SerializeField] private Rigidbody _rigidbody;

    private Animator _animator;
    private Collider _collider;

    private void OnEnable()
    {
        _soldier.Died += OnDied;
    }

    private void OnDisable()
    {
        _soldier.Died -= OnDied;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();

        for (int i = 0; i < _rigidbodies.Length; i++)
            _rigidbodies[i].isKinematic = true;
    }

    private void MakePhysical()
    {
        _animator.enabled = false;
        _collider.enabled = false;
        Destroy(_rigidbody);
      
        for (int i = 0; i < _rigidbodies.Length; i++)
            _rigidbodies[i].isKinematic = false;
    }

    private void OnDied(IDamageable damageable)
    {
        MakePhysical();
    }
}