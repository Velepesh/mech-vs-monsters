using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EnemyCollider : MonoBehaviour, IDamageable, ITarget
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vector3 _offset;

    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> HealthChanged;
    public Vector3 GetPosition() => transform.position + _offset;
    public int Health => _enemy.Health;
    public bool IsDied => _enemy.Health <= 0;

    public void TakeDamage(int damage)
    {
        _enemy.TakeDamage(damage);

        if (_enemy.IsDied)
            Died?.Invoke(_enemy);
    }
}