using UnityEngine;
using UnityEngine.Events;

public class Mine : MonoBehaviour, IDamageable, ITarget, IAward
{
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Health _health;
    [SerializeField] private int _award;
    [SerializeField] private float _offsetX = 1.2f;

    public Health Health => _health;
    public int Award => _award;
    public Vector3 Position => transform.position;
    public bool IsDied => _health.Value <= 0;

    public event UnityAction<IDamageable> Died;

    private void OnValidate()
    {
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
        _award = Mathf.Clamp(_award, 0, int.MaxValue);
        _offsetX = Mathf.Clamp(_offsetX, 0f, float.MaxValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);

            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (_health.Value <= 0)
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