using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LimbShop : MonoBehaviour
{
    [SerializeField] private RobotBuilder _robotBuilder;
    [SerializeField] private List<Limb> _limbs;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Player _player;
    [SerializeField] private LimbView _template;
    [SerializeField] private GameObject _itemContainer;
    //[SerializeField] private Button _closeButton;

    private List<LimbView> _limbViews = new List<LimbView>();
    private Limb _currentLimb;

    public event UnityAction LimbSelected;
   // public event UnityAction<LimbShop> Opened;

    private void Awake()
    {
        //Opened?.Invoke(this);
        
        for (int i = 0; i < _limbs.Count; i++)
        {
            Limb limb = _limbs[i];
            AddItem(limb);

            if (limb.IsSelect)
                _currentLimb = limb;
        }
    }

    //private void OnEnable()
    //{
    //    _closeButton.onClick.AddListener(OnCloseButtonClick);
    //}

    private void OnDisable()
    {
        //_closeButton.onClick.RemoveListener(OnCloseButtonClick);

        for (int i = 0; i < _limbViews.Count; i++)
            _limbViews[i].LimbButtonClick -= OnLimbButtonClick;
    }

    //private void OnCloseButtonClick()
    //{
    //    gameObject.SetActive(false);
    //}

    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
    private void AddItem(Limb limb)
    {
        LimbView view = Instantiate(_template, _itemContainer.transform);
        view.LimbButtonClick += OnLimbButtonClick;
        _limbViews.Add(view);
        view.Render(limb);
    }

    private void OnLimbButtonClick(Limb limb, LimbView view)
    {
        if(limb.IsBuyed == false)  
            TrySellLimb(limb, view);

        if (limb.IsBuyed)
        {
            if (_currentLimb != null)
            {
                _player.RemoveHealth(_currentLimb.Health);
                _currentLimb.Unselecte();

                ChangePlayerHealth(limb);
            }
            else
            {
                ChangePlayerHealth(limb);
            }

            //SelectLimb(GetLimbIndex(limb));
            _robotBuilder.SelectLimb(limb.Type, GetLimbIndex(limb));
            LimbSelected?.Invoke();
        }
    }

    private void ChangePlayerHealth(Limb limb)
    {  
        _currentLimb = limb;
        _currentLimb.Selecte();
        _player.AddHealth(_currentLimb.Health);
    }

    private void TrySellLimb(Limb limb, LimbView view)//Limb Selected
    {
        if (limb.Price <= _wallet.Money)
        {
            _wallet.BuyLimb(limb, limb.Price);

            limb.Buy();
            view.Unlock();
            //view.SellButtonClick -= OnSellButtonClick;
        }
    }
    private void SelectLimb(int index)
    {
       // if (_currentLimb != null)//грузить текущий
       //     _currentLimb.gameObject.SetActive(false);

       //// _currentLimb = _playerLimb[index];
       // _currentLimb.gameObject.SetActive(true);
      
        //LimbSelected?.Invoke();
    }

    private int GetLimbIndex(Limb limb)
    {
        int index = -1;

        for (int i = 0; i < _limbs.Count; i++)
            if (_limbs[i] == limb)
                index = i;

        if (index < 0)
            throw new ArgumentException();

        return index;
    }

    //private void EnableSelecteView()
    //{
    //    _skinViews[_currentTemplateIndex].SelecteView();
    //}

    //private void DisableSelecteView()
    //{
    //    _skinViews[_currentTemplateIndex].DisableView();
    //}
}