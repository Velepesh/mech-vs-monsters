using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbShopsHolder : MonoBehaviour
{
    [SerializeField] private Button _emptySpaceButton;
    [SerializeField] private List<ChooseLimbButton> _chooseLimbButtons;

    private LimbShop _openedShop;
    private bool _isSpaceButtonActive;

    private void Start()
    {
        _isSpaceButtonActive = true;
        DisableEmptySpaceButton();
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

    private void OnOpened(LimbShop shop)
    {
        if (_openedShop != shop)
        {
            CloseOpenedShop();

            _openedShop = shop;
        }

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
}