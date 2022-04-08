using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
public class LimbView : MonoBehaviour
{
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _specificationText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;
    [SerializeField] private GameObject _lockedView;

    private Animator _animator;
    private Limb _limb;
    private int _money;

    public event UnityAction<Limb, LimbView> LimbButtonClick;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);

        //if (_limb != null)
        //    TryFlicker(_limb, _money);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Flicker()
    {
        _animator.SetTrigger(AnimatorFlickerController.States.Flicker);
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

    private void OnButtonClick()
    {
        LimbButtonClick?.Invoke(_limb, this);
    }
}