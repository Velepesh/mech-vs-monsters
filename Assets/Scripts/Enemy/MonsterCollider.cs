using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class MonsterCollider : MonoBehaviour, IDamageable, ITarget
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Vector3 _offset;

    private readonly int _award = 0;
    public int Award => _award;

    public event UnityAction<IDamageable> Died;
    public Vector3 Position => transform.position + _offset;
    public Health Health => _monster.Health;
    public bool IsDied => _monster.Health.Value <= 0;
    public Monster Monster => _monster;

    public void TakeDamage(int damage)
    {
        _monster.TakeDamage(damage);

        if (_monster.IsDied)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
    }
}