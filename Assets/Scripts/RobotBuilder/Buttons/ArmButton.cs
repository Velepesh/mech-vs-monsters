using UnityEngine.Events;

public class ArmButton : ChooseLimbButton
{
    public override event UnityAction<LimbShop> Opened;

    public override void Lock()
    {
        LockPanel.SetActive(true);
        Button.interactable = false;
    }

    public override void Unlock()
    {
        LockPanel.SetActive(false);
        Button.interactable = true;
    }

    protected override void Close()
    {
        Shop.gameObject.SetActive(false);
    }

    protected override void Open()
    {
        Opened?.Invoke(Shop);
        Shop.gameObject.SetActive(true);
    }

    protected override void OnButtonClick()
    {
        Open();
    }

}