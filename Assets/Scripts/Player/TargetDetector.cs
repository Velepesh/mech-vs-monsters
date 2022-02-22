using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PlayerCollider> _playerColliders;
    [SerializeField] private PlayerWeaponsHolder _playerWeapons;

    private List<IDamageable> _targets = new List<IDamageable>();

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

    public ITarget GetClosestTarget(Vector3 weaponPosition)
    {
        return SearchClosest(weaponPosition, _targets);
    }

    private void AddFightTarget(Godzilla godzilla)
    {
        godzilla.Died += OnDied;
        _targets.Add(godzilla);
        UpdateGunsTarget();
    }

    private void OnFought(Godzilla godzilla)
    {
        AddFightTarget(godzilla);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (damageable is ITarget target)
            {
                ITarget closestTarget = GetClosestPlayerCollider(target.Position);

                _targets.Add(damageable);
                damageable.Died += OnDied;

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
            foreach (Weapon weapon in _playerWeapons.Weapons)
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
            Vector3 weaponPosition = weapon.transform.position;
            ITarget target = GetClosestTarget(weaponPosition);

            if (weapon is Machinegun machinegun)
                machinegun.SetTarget(target, _player);
            else if (weapon is Rocketgun rocketgun)
                rocketgun.SetTarget(target, _player);
        }
    }

    private ITarget GetClosestPlayerCollider(Vector3 enemyPosition)
    {
        return SearchClosest(enemyPosition, _playerColliders);
    }

    private ITarget SearchClosest<T>(Vector3 weaponPosition, List<T> targets)
    {
        ITarget closest = null;
        float maxRange = float.MaxValue;

        foreach (ITarget target in targets)
        {
            float distance = Vector3.Distance(target.Position, weaponPosition);

            if (distance < maxRange)
            {
                maxRange = distance;
                closest = target;
            }
        }

        return closest;
    }
}