using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Player _player;
    [SerializeField] private Game _game;
    [SerializeField] private List<EnemySpawnPoint> _enemySpawnPoints;
    [SerializeField] private bool _isSpawnWhenStart;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private float _startPlayerPositionZ;
    private int _spawned;
    private Vector3 _offset;
    private List<Enemy> _enemies = new List<Enemy>();
    private List<EnemySquad> _enemySquads = new List<EnemySquad>();
    private bool _isAllEnemiesDied => _enemies.Count == 0 && _enemySquads.Count == 0;

    private void Start()
    {
        _offset.x = _player.Position.x - transform.position.x;
        _startPlayerPositionZ = _player.Position.z;
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
        _game.LevelStarted += OnLevelStarted;
        _game.Fought += OnFought;
    }

    private void OnDisable()
    {
        _player.Died += OnPlayerDied;
        _game.LevelStarted -= OnLevelStarted;
        _game.Fought -= OnFought;
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay)
        {
            Spawn();
            _timeAfterLastSpawn = 0;
        }

        if (_currentWave.Count <= _spawned)
            _currentWave = null;
    }

    public void StopEnemies()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].StopEnemy();

        for (int i = 0; i < _enemySquads.Count; i++)
            _enemySquads[i].StopEnemies();

        _currentWave = null;
    }

    public void ContinueEnemiesAttack()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].ContinueAttack();

        for (int i = 0; i < _enemySquads.Count; i++)
            _enemySquads[i].ContinueEnemiesAttack();

        SetWave(_currentWaveNumber);
    }

    private void OnLevelStarted()
    {
        SetWave(_currentWaveNumber);

        if(_isSpawnWhenStart)
            _timeAfterLastSpawn = _currentWave.Delay - 1;
    }

    private void OnFought(FightType type)
    {
        StopSpawn();
        DisableEnemies();
    }

    private void StopSpawn()
    {
        _currentWave = null;
    }

    private void DisableEnemies()
    {
        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].LoseTarget();

        for (int i = 0; i < _enemySquads.Count; i++)
            _enemySquads[i].LoseTarget();
    }

    private void Spawn()
    {
        if (CanSpawn() == false)
            return;

        EnemySpawnPoint enemySpawnPoint = GetSpawnPoint();

        while (enemySpawnPoint.CanSpawn == false)
            enemySpawnPoint = GetSpawnPoint();

        GameObject template = _currentWave.Templates[_spawned];

        float offsetPositionZ = _startPlayerPositionZ - enemySpawnPoint.Position.z;

        Vector3 spawnPosition = new Vector3(enemySpawnPoint.Position.x + _offset.x + _player.Position.x, template.transform.position.y, _player.Position.z - offsetPositionZ);

        if (template.TryGetComponent(out Enemy enemy))
        {
            enemy = Instantiate(template, spawnPosition, template.transform.rotation).GetComponent<Enemy>();
            enemy.Init(_player);
            enemySpawnPoint.Init(enemy);
            enemy.Died += OnDied;
            _enemies.Add(enemy);
        }
        else if(template.TryGetComponent(out EnemySquad enemySquad))
        {
            enemySquad = Instantiate(template, spawnPosition, template.transform.rotation).GetComponent<EnemySquad>();
            enemySquad.Init(_player);
            enemySpawnPoint.Init(enemySquad);
            enemySquad.SquadDestroyed += OnSquadDestroyed;
            _enemySquads.Add(enemySquad);
        }

        _spawned++;
    }

    private EnemySpawnPoint GetSpawnPoint()
    {
        return _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Count)];
    }

    private bool CanSpawn()
    {
        for (int i = 0; i < _enemySpawnPoints.Count; i++)
        {
            if (_enemySpawnPoints[i].CanSpawn)
                return true;
        }

        return false;
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
    }

    private void NextWave()
    {
        _enemies = new List<Enemy>();
        SetWave(++_currentWaveNumber);
        _spawned = 0;
    }

    private void OnDied(IDamageable damageable)
    {
        if (damageable is Enemy enemy)
        {
            damageable.Died -= OnDied;
            _enemies.Remove(enemy);

            TrySpawnNexWave();
        }
    }

    private void OnPlayerDied(IDamageable damageable)
    {
        StopSpawn();
        damageable.Died -= OnPlayerDied;  
    }

    private void OnSquadDestroyed(EnemySquad enemySquad)
    {
        enemySquad.SquadDestroyed -= OnSquadDestroyed;
        _enemySquads.Remove(enemySquad);
        TrySpawnNexWave();
    }

    private void TrySpawnNexWave()
    {
        if (_isAllEnemiesDied)
        {
            if (_waves.Count > _currentWaveNumber + 1)
                NextWave();
        }
    }
}


[System.Serializable]
public class Wave
{
    public List<GameObject> Templates;
    public float Delay;
    public int Count => Templates.Count;

    public void RemoveTemplate(GameObject template)
    {
        Templates.Remove(template);
    }
}