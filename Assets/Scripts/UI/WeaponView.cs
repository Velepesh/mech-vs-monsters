using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _view;
    [SerializeField] private GameObject _lockedByMoneyView;

    private Animator _animator;

    public event UnityAction<WeaponView> WeaponButtonClick;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void TryLockItem(AdditionalWeapon weapon, PlayerAdditionalWeapon playerAdditionalWeapon, int money)
    {
        if(playerAdditionalWeapon.IsVisible)
        {
            if (weapon.IsBuyed)
                Hide();
            else
                LockByMoney(weapon, money);
        }
        else
        {
            Hide();
        }
    }

    public void Render(AdditionalWeapon weapon, PlayerAdditionalWeapon playerAdditionalWeapon, int money)
    {
        TryLockItem(weapon, playerAdditionalWeapon, money);

        _price.text = weapon.Price.ToString();
        _damageText.text = weapon.Damage.ToString();
        _icon.sprite = weapon.Icon;
    }

    public void Hide()
    {
        _view.SetActive(false);
    }

    public void Show()
    {
        _view.SetActive(true);
    }

    private void LockByMoney(AdditionalWeapon weapon, int money)
    {
        Show();
        TryFlicker(weapon, money);
        _lockedByMoneyView.SetActive(true);
    }

    private void TryFlicker(AdditionalWeapon weapon, int money)
    {
        if (money >= weapon.Price)
            _animator.SetTrigger(AnimatorFlickerController.States.Flicker);
    }

    private void OnButtonClick()
    {
        WeaponButtonClick?.Invoke(this);
    }
}