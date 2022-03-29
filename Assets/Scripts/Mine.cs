using UnityEngine;
using UnityEngine.Events;

public class Mine : MonoBehaviour, IDamageable, IDieingPolicy
{
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private int _health;
    [SerializeField] private int _award;
    [SerializeField] private float _offsetX = 1.2f;

    public int Health => _health;
    public int Award => _award;

    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> HealthChanged;

    private void OnValidate()
    {
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
        _health = Mathf.Clamp(_health, 0, int.MaxValue);
        _award = Mathf.Clamp(_award, 0, int.MaxValue);
        _offsetX = Mathf.Clamp(_offsetX, 0f, float.MaxValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);

            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            _health = 0;

        HealthChanged?.Invoke(_health);

        if (_health == 0)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
        Explosion();
    }

    private void Explosion()
    {

        Instantiate(_explosionEffect.gameObject, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}