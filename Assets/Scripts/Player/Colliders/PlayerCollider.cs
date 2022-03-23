using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Collider))]
public class PlayerCollider : MonoBehaviour
{
    private Player _player;
    private Collider _collider;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
    }

    private void OnDied(IDamageable damageable)
    {
        _collider.enabled = false;
    }
}