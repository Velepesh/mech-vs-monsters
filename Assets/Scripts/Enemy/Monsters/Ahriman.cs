using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Ahriman : Monster, IDamageable, ITarget, IAward
{
    [SerializeField] private List<Machinegun> _machineguns;
    [SerializeField] private MonsterCollider _monsterCollider;
    [SerializeField] private float _moveToTargetPointTime;
    [SerializeField] private Transform _targetFightPoint;

    private Player _player;

    public new Vector3 Position => _monsterCollider.Position;

    public new event UnityAction AttackStarted;

    public new void Fight(Player player)
    {
        _player = player;
        EnableModel();
        SetWeaponsTarget();
        AttackStarted?.Invoke();
    }

    public void PrepearedForFight()
    {
        Move();
    }

    public void Move()
    {
        StartCoroutine(Move(_moveToTargetPointTime));
    }

    private IEnumerator Move(float duration)
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, _targetFightPoint.position, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetWeaponsTarget()
    {
        for (int i = 0; i < _machineguns.Count; i++)
            _machineguns[i].SetTarget(_player, this);
    }
}