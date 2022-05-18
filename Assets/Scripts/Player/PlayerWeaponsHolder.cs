using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWeaponsHolder : MonoBehaviour
{
    [SerializeField] private List<Weapon> _defaultWeapons;
    [SerializeField] private List<Weapon> _automaticAimWeapons;
    [SerializeField] private List<LaserGun> _laserGuns;

    private Player _player;
    private FightType _type;
    public IReadOnlyList<Weapon> AutomaticAimWeapons => _automaticAimWeapons;
    public IReadOnlyList<Weapon> DefaultWeapons => _defaultWeapons;
    public IReadOnlyList<Weapon> LaserGuns => _laserGuns;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Won += OnWon;
        _player.Fought += OnFought;
        _player.FightWon += OnFightWon;
        _player.Prepeared += OnPrepeared;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.Fought -= OnFought;
        _player.FightWon -= OnFightWon;
        _player.Prepeared -= OnPrepeared;
    }

    public void StartWeaponShooting()
    {
        foreach (Weapon weapon in _automaticAimWeapons)
            weapon.StartShooting();

        foreach (Weapon weapon in _defaultWeapons)
            weapon.StartShooting();
    }

    public void StopWeaponShooting()
    {
        foreach (Weapon weapon in _automaticAimWeapons)
            weapon.StopShooting();

        foreach (Weapon weapon in _defaultWeapons)
            weapon.StopShooting();
    }

    public Weapon GetAutoAimWeapon(int index)
    {
        return _automaticAimWeapons[index];
    }

    public Weapon GetDefaultWeapon(int index)
    {
        return _defaultWeapons[index];
    }

    private void OnWon()
    {
        StopWeaponShooting();
        StopLaserGunShooting();
    }

    private void OnFought(Monster monster)
    {
        if (_type == FightType.Hands)
            StartWeaponShooting();
    }

    private void OnFightWon()
    {
        StopWeaponShooting();
        StopLaserGunShooting();
    }

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
    {
        _type = type;
        StopWeaponShooting();
     
        if (_type != FightType.Hands)
            StartLaserGunShooting();
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