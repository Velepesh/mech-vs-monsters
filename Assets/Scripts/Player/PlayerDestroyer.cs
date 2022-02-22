using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject _model;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
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
        _model.SetActive(false);
    }
}