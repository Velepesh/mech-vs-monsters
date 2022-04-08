using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerWeaponsHolder _playerWeapons;
    [SerializeField] private LayerMask _obstacleLayerMask;
    [SerializeField] private MoverOptions _moverOptions;

    private List<IDamageable> _targets = new List<IDamageable>();

    private void OnEnable()
    {
        _player.Fought += OnFought;
        _player.Fell += OnFell;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _player.Fought -= OnFought;
        _player.Fell -= OnFell;
        _player.Died -= OnPlayerDied;
    }

    public ITarget GetClosestTarget(Vector3 weaponPosition)
    {
        return SearchClosest(weaponPosition, _targets);
    }

    public ITarget GetMaxHealthTarget(Vector3 weaponPosition)
    {
        return SearchMaxHealthTarget(weaponPosition, _targets);
    }

    public ITarget GetClosetEnemy(Vector3 weaponPosition)
    {
        return SearchEnemyTarget(weaponPosition, _targets);
    }

    private void AddFightTarget(Monster monster)
    {
        monster.Died += OnDied;
        _targets.Add(monster);
        UpdateGunsTarget();
    }

    private void OnFought(Monster monster)
    {
        LoseTargetForEnemy();

        ClearTargets();
        AddFightTarget(monster);
    }

    private void ClearTargets()
    {
        _targets.Clear();
    }

    private void OnFell()
    {
        ClearTargets();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (damageable is ITarget target)
            {
                _targets.Add(damageable);
                damageable.Died += OnDied;

                if (damageable is Enemy enemy)
                    enemy.Init(_player);
                //else if (damageable is MonsterCollider enemyCollider)
                //    enemyCollider.Monster.Init(_player);

                UpdateGunsTarget();
            }
        }
    }

    private void OnDied(IDamageable damageable)
    {
        if(damageable is IAward award)
            _player.AddMoney(award.Award);

        _targets.Remove(damageable);
        damageable.Died -= OnDied;

        UpdateGunsTarget();
    }

    private void OnPlayerDied(IDamageable damageable)
    {
        LoseTargetForEnemy();
    }


    private void LoseTargetForEnemy()
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
            foreach (Weapon weapon in _playerWeapons.AutomaticWeapons)
            {
                if (weapon.Target == null || weapon.Target.IsDied)
                    ApplyClosestTarget(weapon);
            }
        }
    }

    private void ApplyClosestTarget(Weapon weapon)
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

    private ITarget SearchMaxHealthTarget<T>(Vector3 weaponPosition, List<T> targets)
    {
        ITarget closest = null;
        float maxHealth = 0f;
        List<ITarget> otherTargets = new List<ITarget>();

        foreach (ITarget target in targets)
        {
            if (target is Enemy enemy)
            {
                float health = enemy.Health.Value;

                if (maxHealth < health)
                {
                    maxHealth = health;
                    closest = target;
                }
            }
            else
            {
                otherTargets.Add(target);
            }
        }

        if (closest == null)
            closest = SearchClosest(weaponPosition, otherTargets);

        return closest;
    }

    private ITarget SearchEnemyTarget<T>(Vector3 weaponPosition, List<T> targets)
    {
        ITarget enemy = null;
        List<ITarget> vehicales = new List<ITarget>();
        List<ITarget> soldiers = new List<ITarget>();
        List<ITarget> otherTargets = new List<ITarget>();

        foreach (ITarget target in targets)
        {
            if(target is IDamageable)
            {
                if (target is Vehicle vehicle)
                    vehicales.Add(vehicle);
                else if (target is Soldier soldier)
                    soldiers.Add(soldier);
                else
                    otherTargets.Add(target);
            }
        }

        if (vehicales.Count > 0 && _moverOptions.IsNearEnemy)
        {
            enemy = SearchClosest(weaponPosition, vehicales);
            
            if (_moverOptions.IsNearObstacle)
                enemy = SearchClosest(weaponPosition, otherTargets);
        }
        else if (soldiers.Count > 0 && _moverOptions.IsNearEnemy)
            enemy = SearchClosest(weaponPosition, soldiers);
        else
            enemy = SearchClosest(weaponPosition, otherTargets);

        return enemy;
    }
}