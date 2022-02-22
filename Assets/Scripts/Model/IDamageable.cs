using UnityEngine.Events;

public interface IDamageable
{ 
    int Health { get; }
    void TakeDamage(int damage);
    event UnityAction<IDamageable> Died;
    event UnityAction<int> HealthChanged;
}