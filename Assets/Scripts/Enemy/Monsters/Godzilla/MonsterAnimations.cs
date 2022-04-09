using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Monster))]
public class MonsterAnimations : MonoBehaviour
{
    private Monster _monster;
    private Animator _animator;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _monster.Disabled += OnDisabled;
        _monster.Won += OnWon;
        _monster.AttackStarted += OnAttackStarted;
    }

    private void OnDisable()
    {
        _monster.Disabled -= OnDisabled;
        _monster.Won -= OnWon;
        _monster.AttackStarted -= OnAttackStarted;
    }

    private void OnDisabled()
    {
        _animator.SetTrigger(AnimatorMonsterController.States.Death);
    }

    private void OnWon()
    {
        _animator.SetBool(AnimatorMonsterController.States.IsAttack, false);
        _animator.SetTrigger(AnimatorMonsterController.States.Win);
    }

    private void OnAttackStarted()
    {
        _animator.SetBool(AnimatorMonsterController.States.IsAttack, true);
    }
}