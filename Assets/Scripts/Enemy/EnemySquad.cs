using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySquad : MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();

    public event UnityAction<EnemySquad> SquadDestroyed;
    
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if(child.TryGetComponent(out Enemy enemy))
                _enemies.Add(enemy);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].Died += OnDied;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].Died -= OnDied;
    }

    public void StopEnemies()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].StopEnemy();
    }

    public void ContinueEnemiesAttack()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].ContinueAttack();
    }

    public void LoseTarget()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].LoseTarget();
    }

    public void Init(Player player)
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].Init(player);
    }

    private void OnDied(IDamageable damageable)
    {
        if(damageable is Enemy enemy)
        {
            enemy.Died -= OnDied;
            _enemies.Remove(enemy);

            if (_enemies.Count == 0)
                DestroySquad();
        }
    }

    private void DestroySquad()
    {
        SquadDestroyed?.Invoke(this);
    }
}