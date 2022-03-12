using UnityEngine;
using TMPro;

public class MenuAttackView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _attackForceText;

    private int _currentAttackForce;

    private void OnEnable()
    {
        _currentAttackForce = _player.AttackForce;

        _attackForceText.text = _currentAttackForce.ToString();

        _player.AttackForceChanged += OnAttackForceChanged;
    }

    private void OnDisable()
    {
        _player.AttackForceChanged -= OnAttackForceChanged;
    }

    private void OnAttackForceChanged(int attackForce)
    {
        _attackForceText.text = attackForce.ToString();
    }
}