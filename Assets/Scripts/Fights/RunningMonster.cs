using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunningMonster : Monster, IAward
{
    [SerializeField] private List<TimerMonsterCollider> _timerMonsterColliders;
    [SerializeField] private float _moveToTargetPointTime;
    [SerializeField] private Transform _targetFightPoint;
    [SerializeField] private float _rotationSpeed;

    private Player _player;

    public new event UnityAction AttackStarted;
    public new event UnityAction Moved;

    private void OnValidate()
    {
        _moveToTargetPointTime = Mathf.Clamp(_moveToTargetPointTime, 0f, float.MaxValue);
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

    private void Update()
    {
        if (_player != null)
            LookAtTarget(_player.Position);
    }

    public new void Fight(Player player)
    {
        _player = player;
        Debug.Log("Fight");
        Move();
        EnableModel();
    }

    public void Move()
    {
        Moved?.Invoke();
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
        Debug.Log("AttackStarted");
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