using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Godzilla))]
public class GodzillaAnimations : MonoBehaviour
{
    private Godzilla _godzilla;
    private Animator _animator;

    private void Awake()
    {
        _godzilla = GetComponent<Godzilla>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _godzilla.Died += OnDied;
        _godzilla.Won += OnWon;
        _godzilla.Attacked += OnAttacked;
    }

    private void OnDisable()
    {
        _godzilla.Died -= OnDied;
        _godzilla.Won -= OnWon;
        _godzilla.Attacked -= OnAttacked;
    }

    private void OnDied(IDamageable damageable)
    {
        _animator.SetTrigger(AnimatorGodzillaController.States.Death);
    }

    private void OnWon()
    {
        _animator.SetBool(AnimatorGodzillaController.States.IsAttack, false);
        _animator.SetTrigger(AnimatorGodzillaController.States.Win);
    }

    private void OnAttacked()
    {
        _animator.SetBool(AnimatorGodzillaController.States.IsAttack, true);
    }
}