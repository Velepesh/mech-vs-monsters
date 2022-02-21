using UnityEngine;

[RequireComponent(typeof(IDamageable))]
public class DieEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dieEffect;
    [SerializeField] private Transform _point;

    private IDamageable _damageable;

    private void Awake()
    {
        _damageable = GetComponent<IDamageable>();
    }

    private void OnEnable()
    {
        _damageable.Died += OnDied;
    }

    private void OnDisable()
    {
        _damageable.Died -= OnDied;
    }

    private void OnDied(IDamageable damageable)
    {
        GameObject effect = Instantiate(_dieEffect.gameObject, _point.position, _dieEffect.gameObject.transform.rotation);
    }
}