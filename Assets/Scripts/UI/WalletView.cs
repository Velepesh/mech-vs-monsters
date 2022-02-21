using UnityEngine;
using TMPro;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Wallet _wallet;

    private int _currentMoney;

    private void OnEnable()
    {
        _currentMoney = _wallet.Money;

        _moneyText.text = _currentMoney.ToString();

        _wallet.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _currentMoney = money;

        UpdateMoneyView(money);
    }

    private void UpdateMoneyView(int money)
    {
        _moneyText.text = money.ToString();
    }
}