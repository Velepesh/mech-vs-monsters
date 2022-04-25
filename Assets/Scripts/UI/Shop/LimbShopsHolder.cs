using System.Collections.Generic;
using UnityEngine;

public class LimbShopsHolder : MonoBehaviour
{
    [SerializeField] private List<ChooseLimbButton> _chooseLimbButtons;
    [SerializeField] private HeadButton _headButton;
    [SerializeField] private ArmButton _armButton;
    [SerializeField] private Game _game;
    [SerializeField] private Wallet _wallet;

    private const string LAST_OPENED_SHOP_ID = "LastOpenedShopID";
    private readonly int _defaultIndex = -1;
    private readonly int _secondLevel = 2;
    private readonly int _thirdLevel = 3;

    private LimbShop _openedShop;
    private int _currentShopID => PlayerPrefs.GetInt(LAST_OPENED_SHOP_ID, _defaultIndex);

    private void Awake()
    {
        if(_game.CurrentLevel == _secondLevel && _headButton.LimbShop.IsHeadSelected == false)
            OpenShopByType(_headButton);
        else if (_game.CurrentLevel == _thirdLevel && _headButton.LimbShop.IsArmSelected == false)
            OpenShopByType(_armButton);
        else if (_currentShopID > _defaultIndex)
            OpenLastShop();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _chooseLimbButtons.Count; i++)
            _chooseLimbButtons[i].Opened += OnOpened;

        _wallet.MoneyChanged += OnMoneyChanged;

        OnMoneyChanged(_wallet.Money);
    }

    private void OnDisable()
    {
        for (int i = 0; i < _chooseLimbButtons.Count; i++)
            _chooseLimbButtons[i].Opened -= OnOpened;

        _wallet.MoneyChanged -= OnMoneyChanged;
    }

    private void OnOpened(LimbShop shop, ChooseLimbButton chooseLimbButton)
    {
        if (_openedShop != shop)
        {
            CloseOpenedShop();

            _openedShop = shop;
        }

        SaveLastOpenedShop(chooseLimbButton);
    }


    private void OpenShopByType(ChooseLimbButton chooseLimb)
    {
        chooseLimb.Open();
    }

    private void OpenLastShop()
    {
        _openedShop = _chooseLimbButtons[_currentShopID].LimbShop;
        _chooseLimbButtons[_currentShopID].Open();
    }

    private void CloseOpenedShop()
    {
        if (_openedShop != null)
            _openedShop.CloseShop();
    }

    private void SaveLastOpenedShop(ChooseLimbButton chooseLimbButton)
    {
        for (int i = 0; i < _chooseLimbButtons.Count; i++)
        {
            if (_chooseLimbButtons[i] == chooseLimbButton)
            {
                PlayerPrefs.SetInt(LAST_OPENED_SHOP_ID, i);
                return;
            }
        }
    }

    private void OnMoneyChanged(int money)
    {
        for (int i = 0; i < _chooseLimbButtons.Count; i++)
            _chooseLimbButtons[i].UpdateFlicker(_wallet.Money);
    }
}