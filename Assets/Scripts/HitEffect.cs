using UnityEngine;

[RequireComponent(typeof(PlayerAttackCollider))]
public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;

    private PlayerAttackCollider _playerCollider;
    
    private void Awake()
    {
        _playerCollider = GetComponent<PlayerAttackCollider>();
    }

    private void OnEnable()
    {
        _playerCollider.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _playerCollider.Damaged -= OnDamaged;
    }

    private void OnDamaged(Vector3 position, Quaternion rotation)
    {
        Instantiate(_hitEffect.gameObject, position, rotation);
    }
}