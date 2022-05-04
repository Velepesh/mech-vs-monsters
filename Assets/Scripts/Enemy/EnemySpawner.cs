using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Player _player;
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _finish;
    [SerializeField] private GroundGenerator _groundGenerator;
    [SerializeField] private float _offsetFightSpawnZ;

    private Wave _currentWave;
    private int _currentWaveNumber = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private List<Enemy> _enemies = new List<Enemy>();

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
        _finish.SetActive(false);
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
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

    private void OnLevelStarted()
    {
        SetWave(_currentWaveNumber);
    }

    private void Spawn()
    {
        GameObject template = _currentWave.Templates[_spawned];
        Enemy enemy = Instantiate(template, new Vector3(0f, 0f, _player.Position.z) + new Vector3(0f, template.transform.position.y, 60f), template.transform.rotation).GetComponent<Enemy>();//заменить playerPosition
        _enemies.Add(enemy);
        enemy.Init(_player);
        enemy.Died += OnDied;
        _spawned++;
    }

    private void SpawnFinish()
    {
        Vector3 finishPosition = _finish.transform.position;
        Vector3 targetPosition = new Vector3(finishPosition.x, finishPosition.y, _groundGenerator.GetFightGroundPositionZ() - _offsetFightSpawnZ);
        _finish.transform.position = targetPosition;
        _finish.SetActive(true);
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

            if (_waves.Count <= _currentWaveNumber + 1)
            {
                if (_enemies.Count == 0)
                    SpawnFinish();
            }
            else
            {
                NextWave();
            }
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