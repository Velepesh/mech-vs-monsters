using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class GroundGenerator : MonoBehaviour//Передавать ground Player, и в нужный момент спавн???
{
    [SerializeField] private List<Ground> _templates;
    [SerializeField] private Player _player;
    [SerializeField] private float _startSpawnPositionZ;
    [SerializeField] private int _tileLength = 50;
    [SerializeField] private int _activePlatforms = 5;

    private float _spawnPointZ = 0f;
    private bool _isFinish;

    private Queue<Ground> _activeLevelPlatforms = new Queue<Ground>();

    private void Start()
    {
        TryCreateTemplate(GroundType.Start);
    }

    private void Update()
    {
        float positionZ = _player.transform.position.z;

        if (positionZ > _spawnPointZ - (_activePlatforms * _tileLength))
            TryCreateTemplate(GroundType.Default);

        if (positionZ - (_tileLength * 2) >= _activeLevelPlatforms.Peek().transform.position.z - _startSpawnPositionZ)
            DeletePlatform();
    }

    public float GetFightGroundPositionZ()
    {
        Ground ground = GetTargetGround();

        return ground.transform.position.z - _startSpawnPositionZ;
    }

    private void TryCreateTemplate(GroundType type)
    {
        var template = GetRandomTemplate(type);

        if (template == null)
            return;

        Vector3 targetPosition = template.gameObject.transform.position + (transform.forward * (_spawnPointZ + _startSpawnPositionZ));
        
        var levelPlatform = Instantiate(template, targetPosition, Quaternion.identity, transform);
        _activeLevelPlatforms.Enqueue(levelPlatform);

        _spawnPointZ += _tileLength;

        if(_isFinish)
            template.DisableProps();
    }

    private Ground GetTargetGround()
    {
        List<Ground> grounds = new List<Ground>();
        Ground targetGround = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out Ground ground))
                grounds.Add(ground);
        }

        for (int i = 0; i < grounds.Count; i++)
        {
            Ground ground = grounds[i];
            float distanceToBorderZ = _tileLength / 2;
            float targetPlayerPositionZ = _player.Position.z  + _tileLength;
            float groundPositionZ = ground.transform.position.z - _startSpawnPositionZ;
            
            if ((targetPlayerPositionZ >= groundPositionZ - distanceToBorderZ)
                && (targetPlayerPositionZ <= groundPositionZ + distanceToBorderZ))
            {
                targetGround = grounds[i];

                for (int j = i; j < grounds.Count; j++)
                    grounds[j].DisableProps();
               
                break;
            }
        }

        if (targetGround != null)
            return targetGround;
        else
            throw new ArgumentException();
            
    }
    private Ground GetRandomTemplate(GroundType type)
    {
        var variants = _templates.Where(levelPlatform => levelPlatform.Type == type);

        if (variants.Count() == 1)
            return variants.First();

        var template = variants.ElementAt(UnityEngine.Random.Range(0, variants.Count()));

        return template;
    }

    private void DeletePlatform()
    {
        Destroy(_activeLevelPlatforms.Dequeue().gameObject);
    }
}