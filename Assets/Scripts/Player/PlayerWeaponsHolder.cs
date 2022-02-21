using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWeaponsHolder : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;

    private Player _player;
    public int Count => _weapons.Count;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }
    private void OnEnable()
    {
        _player.Won += OnWon;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
    }

    public Weapon GetWeapon(int index)
    {
        return _weapons[index];
    }

    private void OnWon()
    {
        foreach (Weapon weapon in _weapons)
            weapon.StopShooting();
    }
}