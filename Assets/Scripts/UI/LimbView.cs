using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LimbView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _specificationText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _lockedView;

    private Limb _limb;

    public event UnityAction<Limb, LimbView> LimbButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Limb limb)
    {
        _limb = limb;
        TryLockItem(limb);

        _price.text = limb.Price.ToString();
        _healthText.text = limb.Health.ToString();
        _specificationText.text = limb.SpecificationValue.ToString();
        _icon.sprite = limb.Icon;
    }

    private void TryLockItem(Limb limb)
    {
        if (limb.IsBuyed)
            Unlock();
        else
            Lock();
    }


    public void Lock()
    {
        _lockedView.SetActive(true);
    }

    public void Unlock()
    {
        _lockedView.SetActive(false);
    }

    private IEnumerator StartOpen()
    {
        yield return new WaitForSeconds(0.3f);

        LimbButtonClick?.Invoke(_limb, this);
    }

    private void OnButtonClick()
    {
        StartCoroutine(StartOpen());
    }
}