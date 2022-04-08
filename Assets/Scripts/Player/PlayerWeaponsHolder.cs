using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWeaponsHolder : MonoBehaviour
{
    [SerializeField] private List<Weapon> _automaticWeapons;
    [SerializeField] private RocketLauncher _rocketLauncher;

    private Player _player;
    public int Count => _automaticWeapons.Count;
    public IReadOnlyList<Weapon> AutomaticWeapons => _automaticWeapons;
    public RocketLauncher RocketLauncher => _rocketLauncher;

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

    public void StopShooting()
    {
        foreach (Weapon weapon in _automaticWeapons)
            weapon.StopShooting();
    }

    public void StartShooting()
    {
        foreach (Weapon weapon in _automaticWeapons)
            weapon.StartShooting();
    }
    public Weapon GetWeapon(int index)
    {
        return _automaticWeapons[index];
    }

    private void OnWon()
    {
        StopShooting();
    }
}