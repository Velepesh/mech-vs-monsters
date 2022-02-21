using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    private Enemy _enemy;
    private Animator _animator;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemy.TargetInited += OnTargetInited;
        _enemy.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _enemy.TargetInited -= OnTargetInited;
        _enemy.TargetLost -= OnTargetLost;
    }

    private void OnTargetInited()
    {
        _animator.SetTrigger(AnimatorEnemyController.States.Shoot);
    }

    private void OnTargetLost()
    {
        _animator.SetTrigger(AnimatorEnemyController.States.Idle);
    }
}