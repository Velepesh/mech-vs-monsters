using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemySpawnPointType _type;

    private Enemy _enemy;
    private EnemySquad _enemySquad;

    public bool CanSpawn => _enemy == null && _enemySquad == null;
    public Vector3 Position => transform.localPosition;

    private void OnDisable()
    {
        if (_enemy != null)
            _enemy.Died -= OnDied;

        if (_enemySquad != null)
            _enemySquad.SquadDestroyed -= OnSquadDestroyed;
    }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.Died += OnDied;
    }

    public void Init(EnemySquad enemySquad)
    {
        _enemySquad = enemySquad;
        _enemySquad.SquadDestroyed += OnSquadDestroyed;
    }

    private void OnDied(IDamageable damageable)
    {
        _enemy.Died -= OnDied;
        _enemy = null;
    }

    private void OnSquadDestroyed(EnemySquad enemySquad)
    {
        _enemySquad.SquadDestroyed -= OnSquadDestroyed;
        _enemySquad = null;
    }
}