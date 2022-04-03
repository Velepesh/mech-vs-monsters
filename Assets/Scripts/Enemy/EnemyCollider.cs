using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EnemyCollider : MonoBehaviour, IDamageable, ITarget
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vector3 _offset;

    private readonly int _award = 0;
    public int Award => _award;

    public event UnityAction<IDamageable> Died;
    public Vector3 Position => transform.position + _offset;
    public Health Health => _enemy.Health;
    public bool IsDied => _enemy.Health.Value <= 0;
    public Enemy Enemy => _enemy;

    public void TakeDamage(int damage)
    {
        _enemy.TakeDamage(damage);

        if (_enemy.IsDied)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
    }
}