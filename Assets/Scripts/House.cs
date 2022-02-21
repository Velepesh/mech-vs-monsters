using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour, IDamageable, ITarget, IDieingPolicy
{
    [SerializeField] private int _health;
    [SerializeField] private float _offsetX = 1.2f;

    public int Health => _health;
    public bool IsDied => _health <= 0;

    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> HealthChanged;

    private void OnValidate()
    {
        _health = Mathf.Clamp(_health, 0, int.MaxValue);
        _offsetX = Mathf.Clamp(_offsetX, 0f, float.MaxValue);
    }

    public Vector3 GetPosition() => transform.position + new Vector3(0f, _offsetX, 0f);

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
        Destroy(gameObject);
    }
}
