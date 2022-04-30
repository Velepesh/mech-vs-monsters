using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWeaponsHolder : MonoBehaviour
{
    [SerializeField] private List<Weapon> _automaticWeapons;
    [SerializeField] private List<LaserGun> _laserGuns;
    [SerializeField] private RocketLauncher _rocketLauncher;

    private Player _player;
    public IReadOnlyList<Weapon> AutomaticWeapons => _automaticWeapons;
    public IReadOnlyList<Weapon> LaserGuns => _laserGuns;
    public RocketLauncher RocketLauncher => _rocketLauncher;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
        _player.FightWon += OnFightWon;
        _player.Prepeared += OnPrepeared;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.FightWon -= OnFightWon;
        _player.Prepeared -= OnPrepeared;
    }

    public void StopMainWeaponShooting()
    {
        foreach (Weapon weapon in _automaticWeapons)
            weapon.StopShooting();
    }

    public void StartMainWeaponShooting()
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
        StopMainWeaponShooting();
        StopLaserGunShooting();
    }

    private void OnFightWon()
    {
        StartMainWeaponShooting();
        StopLaserGunShooting();
    }

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
    {
        if (type != FightType.Hands)
        {
            StartLaserGunShooting();
            StopMainWeaponShooting();
        }
    }

    private void StartLaserGunShooting()
    {
        foreach (Weapon weapon in _laserGuns)
            weapon.StartShooting();
    }

    private void StopLaserGunShooting()
    {
        foreach (Weapon weapon in _laserGuns)
            weapon.StopShooting();
    }
}