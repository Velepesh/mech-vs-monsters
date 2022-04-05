using UnityEngine.Events;

public class HeadButton : ChooseLimbButton
{
    public override event UnityAction<LimbShop, ChooseLimbButton> Opened;

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

    public override void Open()
    {
        Opened?.Invoke(Shop, this);
        Shop.gameObject.SetActive(true);
    }

    protected override void Close()
    {
        Shop.gameObject.SetActive(false);
    }

    protected override void OnButtonClick()
    {
        Open();
    }
}