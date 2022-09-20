using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PlayerAdditionalWeapon : MonoBehaviour
{
    [SerializeField] private List<PlayerLimb> _playerLimbs;
    [SerializeField] private Weapon _weapon;

    private bool _isVisible = false;

    public bool IsVisible => _isVisible;

    public event UnityAction ViewEnabled;

    private void Awake()
    {
        for (int i = 0; i < _playerLimbs.Count; i++)
        {
            if (_playerLimbs[i].Limb.IsSelect)
            {
                EnableView();
                return;
            }
        }

        if(_isVisible == false)
            TurnOffWeapon();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _playerLimbs.Count; i++)
            _playerLimbs[i].VisibleBecame += OnVisibleBecame;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _playerLimbs.Count; i++)
            _playerLimbs[i].VisibleBecame -= OnVisibleBecame;
    }

    public void Show()
    {
        _weapon.gameObject.SetActive(true);
    }

    private void OnVisibleBecame()
    {
        EnableView();
    }

    private void EnableView()
    {
        _isVisible = true;
        ViewEnabled?.Invoke();
    }

    private void TurnOffWeapon()
    {
        _weapon.gameObject.SetActive(false);
    }
}