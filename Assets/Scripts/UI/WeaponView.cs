using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _lockedByLevelView;
    [SerializeField] private GameObject _lockedByMoneyView;

    private readonly string _levelText = "Lvl";

    public event UnityAction<WeaponView> WeaponButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    private void TryLockItem(AdditionalWeapon weapon, int level)
    {
        if(level >= weapon.Level)
        {
            UnlockByLevel();

            if (weapon.IsBuyed)
                Unlock();
            else
                LockByMoney();
        }
        else
        {
            LockByLevel();
        }
    }

    public void Render(AdditionalWeapon weapon, int level)
    {
        TryLockItem(weapon, level);

        _price.text = weapon.Price.ToString();
        _level.text = _levelText + " " + weapon.Level.ToString();
        _icon.sprite = weapon.Icon;
    }

    public void Unlock()
    {
        gameObject.SetActive(false);
    }

    private void UnlockByMoney()
    {
        _lockedByMoneyView.SetActive(false);
    }

    private void LockByMoney()
    {
        _lockedByMoneyView.SetActive(true);
    }

    private void LockByLevel()
    {
        _lockedByMoneyView.SetActive(true);
        UnlockByMoney();
    }

    private void UnlockByLevel()
    {
        _lockedByLevelView.SetActive(false);
    }

    private void OnButtonClick()
    {
        WeaponButtonClick?.Invoke(this);
    }
}