using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ChooseLimbButton : MonoBehaviour
{
    [SerializeField] protected LimbShop Shop;
    [SerializeField] protected Button Button;
    [SerializeField] protected GameObject LockPanel;

    public event UnityAction<ChooseLimbButton> LimbSelected;
    public abstract event UnityAction<LimbShop> Opened;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnButtonClick);
        Shop.LimbSelected += OnLimbSelected;
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnButtonClick);
        Shop.LimbSelected -= OnLimbSelected;
    }

    public abstract void Lock();

    public abstract void Unlock();

    protected abstract void OnButtonClick();

    protected abstract void Open();

    protected abstract void Close();

    private void OnLimbSelected()
    {
        LimbSelected?.Invoke(this);
    }
}