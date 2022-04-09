using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LimbShop : MonoBehaviour
{
    [SerializeField] private RobotBuilder _robotBuilder;
    [SerializeField] private List<Limb> _limbs;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Player _player;
    [SerializeField] private LimbView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<LimbView> _limbViews = new List<LimbView>();
    private Limb _currentLimb;

    public bool IsHeadSelected => _robotBuilder.IsHeadSelected;
    public bool IsArmSelected => _robotBuilder.IsArmSelected;

    public event UnityAction LimbSelected;

    private void Awake()
    {
        InitItems();
    }

    private void OnEnable()
    {
        _wallet.MoneyChanged += OnMoneyChanged;

        for (int i = 0; i < _limbViews.Count; i++)
            _limbViews[i].LimbButtonClick += OnLimbButtonClick;

        UpdateFlicker();
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= OnMoneyChanged;

        for (int i = 0; i < _limbViews.Count; i++)
            _limbViews[i].LimbButtonClick -= OnLimbButtonClick;
    }

    private void InitItems()
    {
        for (int i = 0; i < _limbs.Count; i++)
        {
            Limb limb = _limbs[i];
            AddItem(limb);

            if (limb.IsSelect)
                _currentLimb = limb;
        }
    }

    private void AddItem(Limb limb)
    {
        LimbView view = Instantiate(_template, _itemContainer.transform);
        _limbViews.Add(view);
        view.Render(limb);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }

    private void OnLimbButtonClick(Limb limb, LimbView view)
    {
        if(limb.IsBuyed == false)  
            TrySellLimb(limb, view);

        if (limb.IsBuyed)
        {
            if (_currentLimb != null)
            {
                _player.Health.RemoveHealth(_currentLimb.Health);
                _currentLimb.Unselecte();

                if(_currentLimb.Type != LimbType.LEG)
                    _player.RemoveAttackForce(_currentLimb.SpecificationValue);
            }
           
            ChangePlayerHealth(limb);

            if (limb.Type == LimbType.LEG)
                ChangeMoveSpeed(limb);
            else
                ChangeAttackForce(limb);

            _robotBuilder.SelectLimb(limb, GetLimbIndex(limb));
            LimbSelected?.Invoke();

            _currentLimb = limb;
        }

        UpdateFlicker();
    }

    private void OnMoneyChanged(int money)
    {
        UpdateFlicker();
    }

    private void UpdateFlicker()
    {
        for (int i = _limbs.Count - 1; i >= 0; i--)
        {
            Limb limb = _limbs[i];

            if (limb.IsBuyed)
            {
                for (int j = 0; j < i; j++)
                    _limbViews[j].StopFlicker();

                return;
            }
            else
            {
                if (limb.Price <= _wallet.Money)
                    _limbViews[i].Flicker();
                else
                    _limbViews[i].StopFlicker();
            }
        }
    }

    private void ChangePlayerHealth(Limb limb)
    {  
        _player.Health.AddHealth(limb.Health);
    }

    private void ChangeAttackForce(Limb limb)
    {
        _player.AddAttackForce(limb.SpecificationValue);
    }

    private void ChangeMoveSpeed(Limb limb)
    {
        _player.LoadSpeed(limb.SpecificationValue);
    }

    private void TrySellLimb(Limb limb, LimbView view)
    {
        if (limb.Price <= _wallet.Money)
        {
            _wallet.BuyLimb(limb, limb.Price);

            limb.Buy();
            view.Unlock();
        }
    }

    private int GetLimbIndex(Limb limb)
    {
        int index = -1;

        for (int i = 0; i < _limbs.Count; i++)
            if (_limbs[i] == limb)
                index = i;

        if (index < 0)
            throw new ArgumentException();

        return index;
    }
}