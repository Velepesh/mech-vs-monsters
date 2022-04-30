using UnityEngine;

public class Minigun : PlayerAdditionalWeapon
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Machinegun _machinegun;

    private void Awake()
    {
        _machinegun.Shooted += OnShooted;
        _machinegun.LostTarget += OnLostTarget;
    }

    private void OnDisable()
    {
        _machinegun.Shooted -= OnShooted;
        _machinegun.LostTarget -= OnLostTarget;
    }

    private void OnShooted()
    {
        _animator.SetBool(AnimatorWeaponController.States.IsShoot, true);
    }

    private void OnLostTarget()
    {
        _animator.SetBool(AnimatorWeaponController.States.IsShoot, false);
    }
}