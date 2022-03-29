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
    public event UnityAction<int> HealthChanged;
    public Vector3 Position => transform.position + _offset;
    public int Health => _enemy.Health;
    public bool IsDied => _enemy.Health <= 0;
    public Enemy Enemy => _enemy;

    public void TakeDamage(int damage)
    {
        _enemy.TakeDamage(damage);
        HealthChanged?.Invoke(_enemy.Health);

        if (_enemy.IsDied)
            Died?.Invoke(this);
    }
}