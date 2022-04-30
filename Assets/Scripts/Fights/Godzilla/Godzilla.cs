using UnityEngine;
using UnityEngine.Events;

public class Godzilla : Monster, IDamageable, ITarget, IAward
{
    [SerializeField] private Weapon _weapon;

    public IShooteable Weapon => _weapon;

    public event UnityAction Shooted;

    private void OnEnable()
    {
        _weapon.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _weapon.Shooted -= OnShooted;
    }

    private void OnShooted()
    {
        Shooted?.Invoke();
    }
}