using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Attacker _attacker;
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private RobotBuilder _robotBuilder;

    private Animator _animator;
    private int _randomAttack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.Moved += OnMoved;
        _player.Won += OnWon;
        _player.Standed += OnStanded;
        _attacker.Attacked += OnAttacked;
        _rocketLauncher.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _player.Won -= OnWon;
        _player.Moved -= OnMoved;
        _player.Standed -= OnStanded;
        _attacker.Attacked -= OnAttacked;
        _rocketLauncher.Shooted -= OnShooted;
    }

    private void OnMoved()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.Move);
    }

    private void OnShooted()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.ShotRocket);
    }

    private void OnAttacked(float speed)
    {
        _animator.speed = speed;

        if (_robotBuilder.IsArmSelected == false)
        {
            AttackByLeg();
        }
        else
        {
            _randomAttack = Random.Range(0, 2);

            if (_randomAttack == 0)
                AttackByArm();
            else
                AttackByLeg();
        }
    }

    private void AttackByLeg()
    {
        _animator.SetTrigger(AnimatorPlayerController.States.LegAttack);
    }

    private void AttackByArm()
    {
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

    private void EndAttack()
    {
        _animator.speed = 1;
    }
}