using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMover _mover;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemy.Shooted += OnShooted;
        _enemy.TargetLost += OnTargetLost;
        _mover.Moved += OnForwardMoved;
        _mover.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        _enemy.Shooted -= OnShooted;
        _enemy.TargetLost -= OnTargetLost;
        _mover.Moved -= OnForwardMoved;
        _mover.Stopped -= OnStopped;
    }

    private void OnShooted()
    {
        _animator.SetTrigger(AnimatorEnemyController.States.Shoot);
    }

    private void OnTargetLost()
    {
        _animator.SetTrigger(AnimatorEnemyController.States.Idle);
    }

    private void OnForwardMoved()
    {
        _animator.SetBool(AnimatorEnemyController.States.IsMove, true);
    }

    private void OnStopped()
    {
        _animator.SetBool(AnimatorEnemyController.States.IsMove, false);
    }
}