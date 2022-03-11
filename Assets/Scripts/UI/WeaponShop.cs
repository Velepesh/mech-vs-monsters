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

        AddItem(_weapon, _additionalWeapon);
    }

    private void TakeWeapon()
    {
        _robotBuilder.TakeAdditionalWeapon(_additionalWeapon);
        _additionalWeapon.Show();
        _weaponView.Hide();
    }

    private void OnEnable()
    {
        _additionalWeapon.ViewEnabled += OnViewEnabled;
        _weaponView.WeaponButtonClick += OnWeaponButtonClick;
    }
    private void OnDisable()
    {
        _additionalWeapon.ViewEnabled -= OnViewEnabled;
        _weaponView.WeaponButtonClick -= OnWeaponButtonClick;
    }

    private void AddItem(AdditionalWeapon weapon, PlayerAdditionalWeapon playerAdditionalWeapon)
    {
        _weaponView.Render(weapon, playerAdditionalWeapon);
    }

    private void OnWeaponButtonClick(WeaponView view)
    {

        if (_weapon.IsBuyed == false)
           TrySellWeapon(_weapon, view);


        if (_weapon.IsBuyed)
            TakeWeapon();
    }

    private void TrySellWeapon(AdditionalWeapon weapon, WeaponView view)
    {
        if (weapon.Price <= _wallet.Money)
        {
            _wallet.BuyWeapon(weapon, weapon.Price);
            weapon.Buy();
            _additionalWeapon.Show();
            view.Hide();
        }
    }

    private void OnViewEnabled()
    {
                Debug.Log("BecameVisible");
        AddItem(_weapon, _additionalWeapon);
    } 
}