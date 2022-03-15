using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbShopsHolder : MonoBehaviour
{
    [SerializeField] private Button _emptySpaceButton;
    [SerializeField] private List<ChooseLimbButton> _chooseLimbButtons;

    private const string LAST_OPENED_SHOP_ID = "LastOpenedShopID";
    private readonly int _defaultIndex = -1;

    private LimbShop _openedShop;
    private bool _isSpaceButtonActive;
    private int _currentShopID => PlayerPrefs.GetInt(LAST_OPENED_SHOP_ID, _defaultIndex);

    private void Start()
    {
        _isSpaceButtonActive = true;
        DisableEmptySpaceButton();

        if (_currentShopID > _defaultIndex)
        {
            _openedShop = _chooseLimbButtons[_currentShopID].LimbShop;
            _chooseLimbButtons[_currentShopID].Open();
        }
    }

    public void TurnOffEmptySpaceButton()
    {
        _isSpaceButtonActive = false;
    }

    private void OnEnable()
    {
        _emptySpaceButton.onClick.AddListener(OnEmptySpaceButtonClick);

        for (int i = 0; i < _chooseLimbButtons.Count; i++)
            _chooseLimbButtons[i].Opened += OnOpened;
    }

    private void OnDisable()
    {
        _emptySpaceButton.onClick.RemoveListener(OnEmptySpaceButtonClick);

        for (int i = 0; i < _chooseLimbButtons.Count; i++)
            _chooseLimbButtons[i].Opened -= OnOpened;
    }

    private void OnEmptySpaceButtonClick()
    {
        CloseOpenedShop();
        DisableEmptySpaceButton();
    }

    private void OnOpened(LimbShop shop, ChooseLimbButton chooseLimbButton)
    {
        if (_openedShop != shop)
        {
            CloseOpenedShop();

            _openedShop = shop;
        }

        SaveLastOpenedShop(chooseLimbButton);
        EnableEmptySpaceButton();
    }

    private void CloseOpenedShop()
    {
        if (_openedShop != null)
            _openedShop.CloseShop();
    }

    private void EnableEmptySpaceButton()
    {
        if (_isSpaceButtonActive)
            _emptySpaceButton.interactable = true;
    }

    private void DisableEmptySpaceButton()
    {
        _emptySpaceButton.interactable = false;
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
}