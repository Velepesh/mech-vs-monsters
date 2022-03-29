using System.Collections;
using UnityEngine;
using UnityEngine.Events;
    
public class LegButton : ChooseLimbButton
{
    public override event UnityAction<LimbShop, ChooseLimbButton> Opened;
    public override void Lock()
    {
        throw new System.NotImplementedException();
    }

    public override void Unlock()
    {
        throw new System.NotImplementedException();
    }

    public override void Open()
    {
        Opened?.Invoke(Shop, this);
        Shop.gameObject.SetActive(true);
    }

    private IEnumerator StartOpen()
    {
        yield return new WaitForSeconds(0.3f);

        Open();
    }

    protected override void Close()
    {
        Shop.gameObject.SetActive(false);
    }

    protected override void OnButtonClick()
    {
        StartCoroutine(StartOpen());
    }
}