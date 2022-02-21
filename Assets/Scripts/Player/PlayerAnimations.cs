using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private RobotBuilder _robotBuilder;

    private Animator _animator;
    private int _randomAttack;
    private bool _isAttackAnimationEnd = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.MovingStarted += OnMovingStarted;
        _player.Won += OnWon;
        _player.Standed += OnStanded;
        _player.Attacked += OnAttacked;
        _input.Attacked += OnAttacked;
        _rocketLauncher.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.MovingStarted -= OnMovingStarted;
        _player.Standed -= OnStanded;
        _player.Attacked -= OnAttacked;
        _input.Attacked -= OnAttacked;
        _rocketLauncher.Shooted -= OnShooted;
    }

    private void OnMovingStarted()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Move);
    }

    private void OnShooted()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.ShotRocket);
    }

    private void OnAttacked()
    {
        if(_robotBuilder.IsArmSelected == false)
        {
            AttackByLeg();
        }
        else
        {
            _randomAttack = Random.Range(0, 2);

            if(_randomAttack == 0)
                AttackByArm();
            else
                AttackByLeg();
        }
    }

    private void AttackByLeg()
    {
        Debug.Log("LEG");
        _animator.SetTrigger(AnimatorPlayerController.States.LegAttack);
    }

    private void AttackByArm()
    {
        Debug.Log("AttackByArm");
        _animator.SetTrigger(AnimatorPlayerController.States.ArmAttack);
    }

    private void OnStanded()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Stand);
    }

    private void OnWon()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Win);
    }
}