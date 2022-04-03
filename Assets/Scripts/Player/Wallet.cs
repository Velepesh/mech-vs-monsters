using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class Wallet : MonoBehaviour
{
    readonly private string BALANCE = "Balance";
    readonly private string SHOP = "Shop";
    readonly private string WEAPON = "Weapon";

    private int _money;
    private Player _player;

    public int Money => PlayerPrefs.GetInt(BALANCE, 0);

    public event UnityAction<int> MoneyChanged;
    public event UnityAction<string, string, int> Bought;

    private void Awake()
    {
        _money = Money;
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.MoneyAdded += OnMoneyAdded;
    }

    private void OnDisable()
    {
        _player.MoneyAdded -= OnMoneyAdded;
    }

    public void BuyLimb(Limb limb, int price)
    {
        Bought?.Invoke(SHOP, limb.name, price);
        RemoveMoney(price);
    }

    public void BuyWeapon(AdditionalWeapon weapon, int price)
    {
        Bought?.Invoke(WEAPON, weapon.name, price);
        RemoveMoney(price);
    }

    private void AddMoney(int money)
    {
        _money += money;
        MoneyChanged?.Invoke(_money);
        
        SaveBalance();
    }

    private void RemoveMoney(int money)
    {
        _money -= money;
        MoneyChanged?.Invoke(_money);

        SaveBalance();
    }

    private void OnMoneyAdded(int money)
    {
        AddMoney(money);
    }

    private void SaveBalance()
    {
        PlayerPrefs.SetInt(BALANCE, _money);
    }
}