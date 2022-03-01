using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EnemyCollider : MonoBehaviour, IDamageable, ITarget
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Vector3 _offset;

    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> HealthChanged;
    public Vector3 Position => transform.position + _offset;
    public int Health => _enemy.Health;
    public bool IsDied => _enemy.Health <= 0;

    public void TakeDamage(int damage)
    {
        if(_enemy.Health - damage <= 0)
            Died?.Invoke(this);

        _enemy.TakeDamage(damage);
        HealthChanged?.Invoke(_enemy.Health);
    }
}