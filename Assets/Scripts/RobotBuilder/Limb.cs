using UnityEngine;

[CreateAssetMenu(fileName = "Limb", menuName = "Limb", order = 51)]
public class Limb : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;
    [SerializeField] private LimbType _type;
    [SerializeField] private int _health;
    [SerializeField] private bool _isSelect;

    public string Label => _label;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;
    public bool IsSelect => _isSelect;
    public LimbType Type => _type;
    public int Health => _health;

    public void Buy()
    {
        _isBuyed = true;
    }

    public void Selecte()
    {
        _isSelect = true;
    }

    public void Unselecte()
    {
        _isSelect = false;
    }
}