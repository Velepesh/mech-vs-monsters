using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private RobotBuilder _robotBuilder;
    [SerializeField] private PlayerAdditionalWeapon _additionalWeapon;
    [SerializeField] private AdditionalWeapon _weapon;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private WeaponView _weaponView;

    private void Start()
    {
        if (_weapon.IsBuyed)
            TakeWeapon();

        AddItem(_weapon, _game.CurrentLevel);
    }

    private void TakeWeapon()
    {
        _robotBuilder.TakeAdditionalWeapon(_additionalWeapon);
        _weaponView.Unlock();
    }

    private void OnEnable()
    {
        _weaponView.WeaponButtonClick += OnWeaponButtonClick;
    }
    private void OnDisable()
    {
        _weaponView.WeaponButtonClick -= OnWeaponButtonClick;
    }

    private void AddItem(AdditionalWeapon weapon, int level)
    {
        _weaponView.Render(weapon, level);
    }

    private void OnWeaponButtonClick(WeaponView view)
    {
        if (_game.CurrentLevel >= _weapon.Level)
        {
            if (_weapon.IsBuyed == false)
                TrySellWeapon(_weapon, view);
        }

        if (_weapon.IsBuyed)
            TakeWeapon();
    }

    private void TrySellWeapon(AdditionalWeapon weapon, WeaponView view)
    {
        if (weapon.Price <= _wallet.Money)
        {
            _wallet.BuyWeapon(weapon, weapon.Price);
            weapon.Buy();
            view.Unlock();
        }
    }
}