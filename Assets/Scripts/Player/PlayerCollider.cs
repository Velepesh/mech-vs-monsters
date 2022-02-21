using UnityEngine;
using UnityEngine.Events;

public class PlayerCollider : MonoBehaviour, IDamageable, ITarget
{
    [SerializeField] private Player _player;

    public event UnityAction<IDamageable> Died;
    public event UnityAction<int> HealthChanged;
    public Vector3 GetPosition() => transform.position;

    public bool IsDied => _player.Health <= 0;
    public int Health => _player.Health;

    public void TakeDamage(int damage)
    {
        _player.TakeDamage(damage);

        if (IsDied)
        {
            _player.Die();
            Died?.Invoke(_player);
        }
    }
}