using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PlayerCollider> _playerColliders;
    [SerializeField] private PlayerWeaponsHolder _playerWeapons;

    private List<Weapon> _weapons = new List<Weapon>();
    private List<IDamageable> _targets = new List<IDamageable>();
    private List<IDamageable> _colliders = new List<IDamageable>();

    private void OnEnable()
    {
        _player.Fought += OnFought;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Fought -= OnFought;
        _player.Died -= OnPlayerDied;
    }

    private void Start()
    {
        for (int i = 0; i < _playerWeapons.Count; i++)
            _weapons.Add(_playerWeapons.GetWeapon(i));

        for (int i = 0; i < _playerColliders.Count; i++)
        {
            if (_playerColliders[i] is IDamageable damageable)
                _colliders.Add(damageable);
        }       
    }

    public bool IsStopDistance(float stopDistance, Vector3 position, out ITarget target)
    {
        ITarget closest = null;
        target = null;

        if (_targets.Count > 0)
        {
            float maxRange = float.MaxValue;

            foreach (ITarget item in _targets)
            {
                if (item is Godzilla == false)
                {
                    float distance = Mathf.Abs(item.GetPosition().z - position.z);

                    if (distance < maxRange)
                    {
                        maxRange = distance;
                        closest = item;
                    }
                }
            }

            if (closest != null)
            {
                target = closest;

                if (target.GetPosition().z <= position.z + stopDistance)
                    return true;
            }
        }

        return false;
    }

    private void AddFightTarget(IDamageable damageable)
    {
        if (damageable is Godzilla)
        {
            damageable.Died += OnDied;
            _targets.Add(damageable);
            UpdateGunsTarget();
        }
    }

    private void OnFought(Transform playerPosition, Godzilla godzilla)
    {
        AddFightTarget(godzilla);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (damageable is ITarget target)
            {
                ITarget closestTarget = GetClosestTarget(target.GetPosition(), _colliders);

                if (damageable is EnemyCollider == false)
                {
                    _targets.Add(damageable);
                    damageable.Died += OnDied;
                }

                if (damageable is Enemy enemy)
                    enemy.Init(closestTarget);

                UpdateGunsTarget();
            }
        }
    }

    private void OnDied(IDamageable damageable)
    {
        _targets.Remove(damageable);
        damageable.Died -= OnDied;

        UpdateGunsTarget();
    }

    private void OnPlayerDied(IDamageable damageable)
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            if (_targets[i] is Enemy enemy)
                enemy.LoseTarget();
        }
    }

    private void UpdateGunsTarget()
    {
        if (_targets.Count > 0)
        {
            foreach (Weapon weapon in _weapons)
            {
                if (weapon.Target == null || weapon.Target.IsDied)
                    ApplyClosesTarget(weapon);
            }
        }
    }

    private void ApplyClosesTarget(Weapon weapon)
    {
        if (weapon != null)
        {
            Vector3 gunPosition = weapon.transform.position;
            ITarget target = GetClosestTarget(gunPosition, _targets);

            if (weapon is Machinegun machinegun)
                machinegun.SetTarget(target, _player);
            else if (weapon is Rocketgun rocketgun)
                rocketgun.SetTarget(target, _player);
        }
    }


    private ITarget GetClosestTarget(Vector3 weaponPosition, List<IDamageable> damageables)
    {
        ITarget closest = null;
        float maxRange = float.MaxValue;

        foreach (ITarget target in damageables)
        {
            float distance = Vector3.Distance(target.GetPosition(), weaponPosition);

            if (distance < maxRange)
            {
                maxRange = distance;
                closest = target;
            }
        }

        return closest;
    }
}