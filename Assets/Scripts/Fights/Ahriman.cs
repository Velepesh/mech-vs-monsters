using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ahriman : Monster, IDamageable, ITarget, IAward
{
    [SerializeField] private Machinegun _machinegun;
    [SerializeField] private MonsterCollider _monsterCollider;
    [SerializeField] private List<TimerMonsterCollider> _timerMonsterColliders;
    [SerializeField] private float _moveToTargetPointTime;
    [SerializeField] private Transform _targetFightPoint;
    [SerializeField] private float _delayBeforeShooting;

    private Player _player;

    public new Vector3 Position => _monsterCollider.Position;

    public new event UnityAction AttackStarted;

    private void OnValidate()
    {
        _moveToTargetPointTime = Mathf.Clamp(_moveToTargetPointTime, 0f, float.MaxValue);
        _delayBeforeShooting = Mathf.Clamp(_delayBeforeShooting, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        for (int i = 0; i < _timerMonsterColliders.Count; i++)
            _timerMonsterColliders[i].TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _timerMonsterColliders.Count; i++)
            _timerMonsterColliders[i].TimerEnded -= OnTimerEnded;
    }

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
        StartCoroutine(StartShooting(_delayBeforeShooting));
    }

    private IEnumerator StartShooting(float duration)
    {
        yield return new WaitForSeconds(duration);

        _machinegun.SetTarget(_player, this);
    }

    private void OnTimerEnded(TimerMonsterCollider timerMonsterCollider)
    {
        if (_timerMonsterColliders.Count > 0)
        {
            _timerMonsterColliders.Remove(timerMonsterCollider);
            timerMonsterCollider.gameObject.SetActive(false);
            timerMonsterCollider.TimerEnded -= OnTimerEnded;

            if (_timerMonsterColliders.Count == 0)
                Die();

        }  
    }
}