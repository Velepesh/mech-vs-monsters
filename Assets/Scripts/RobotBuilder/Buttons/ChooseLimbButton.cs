using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class ChooseLimbButton : MonoBehaviour
{
    [SerializeField] protected LimbShop Shop;
    [SerializeField] protected Button Button;
    [SerializeField] protected GameObject LockPanel;
    [SerializeField] protected Animator Animator;

    public event UnityAction<ChooseLimbButton> LimbSelected;
    public abstract event UnityAction<LimbShop, ChooseLimbButton> Opened;

    public LimbShop LimbShop => Shop;

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

    public void UpdateFlicker(int money)
    {
        bool isFlicker = false;

        for (int i = 0; i < Shop.Limbs.Count; i++)
        {
            Limb limb = Shop.Limbs[i];

            if (limb.IsBuyed == false && limb.Price <= money)
            {
                Flicker();
                return;
            }
        }

        if (isFlicker == false)
            StopFlicker();
    }

    public abstract void Lock();

    public abstract void Unlock();

    public abstract void Open();
    
    protected abstract void OnButtonClick();

    protected abstract void Close();

    private void OnLimbSelected()
    {
        LimbSelected?.Invoke(this);
    }

    private void Flicker()
    {
        Animator.SetBool(AnimatorFlickerController.States.IsFlicker, true);
    }

    private void StopFlicker()
    {
        Animator.SetBool(AnimatorFlickerController.States.IsFlicker, false);
    }
}