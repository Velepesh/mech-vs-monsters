using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _enemy.Shooted += OnShooted;
        _enemy.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _enemy.Shooted -= OnShooted;
        _enemy.TargetLost -= OnTargetLost;
    }

    private void OnShooted()
    {
        _animator.SetTrigger(AnimatorEnemyController.States.Shoot);
    }

    private void OnTargetLost()
    {
        _animator.SetTrigger(AnimatorEnemyController.States.Idle);
    }
}