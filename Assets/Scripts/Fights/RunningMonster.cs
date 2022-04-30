using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunningMonster : Monster, IAward
{
    [SerializeField] private TimerMonsterCollider _timerMonsterCollider;
    [SerializeField] private float _moveToTargetPointTime;
    [SerializeField] private Transform _targetFightPoint;
    [SerializeField] private float _rotationSpeed;

    private Player _player;
    private IEnumerator _moveCoroutine;

    public new event UnityAction AttackStarted;
    public new event UnityAction Moved;

    private void OnValidate()
    {
        _moveToTargetPointTime = Mathf.Clamp(_moveToTargetPointTime, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        _timerMonsterCollider.TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        _timerMonsterCollider.TimerEnded -= OnTimerEnded;
    }

    private void Update()
    {
        if (_player != null)
            LookAtTarget(_player.Position);
    }

    public new void Fight(Player player)
    {
        _player = player;
        Move();
        EnableModel();
    }

    public void Move()
    {
        Moved?.Invoke();
        _moveCoroutine = Move(_moveToTargetPointTime);
        StartCoroutine(_moveCoroutine);
    }

    private IEnumerator Move(float duration)
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Vector3 targetPosition = new Vector3(_targetFightPoint.position.x, transform.position.y, _targetFightPoint.position.z);
            transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        AttackStarted?.Invoke();
    }
    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

        transform.rotation = lookAt;
    }

    private void OnTimerEnded(TimerMonsterCollider timerMonsterCollider)
    {
        StopCoroutine(_moveCoroutine);
        Die();
    }
}