using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbShopsHolder : MonoBehaviour
{
    [SerializeField] private Button _emptySpaceButton;
    [SerializeField] private List<LimbShop> _limbShops;
    [SerializeField] private List<ChooseLimbButton> _chooseLimbButtons;

    private LimbShop _openedShop;

    private void Start()
    {
        DisableEmptySpaceButton();
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
            EnableEmptySpaceButton();
        }
    }

    private void CloseOpenedShop()
    {
        if (_openedShop != null)
            _openedShop.CloseShop();
    }

    private void EnableEmptySpaceButton()
    {
        _emptySpaceButton.gameObject.SetActive(true);
    }

    private void DisableEmptySpaceButton()
    {
        _emptySpaceButton.gameObject.SetActive(false);
    }
}