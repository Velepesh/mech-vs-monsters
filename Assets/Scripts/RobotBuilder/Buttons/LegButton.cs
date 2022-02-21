using UnityEngine.Events;
    
public class LegButton : ChooseLimbButton
{
    public override event UnityAction<LimbShop> Opened;
    public override void Lock()
    {
        throw new System.NotImplementedException();
    }

    public override void Unlock()
    {
        throw new System.NotImplementedException();
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