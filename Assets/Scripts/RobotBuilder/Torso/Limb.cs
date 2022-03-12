using UnityEngine;

[CreateAssetMenu(fileName = "Limb", menuName = "Limb", order = 51)]
public class Limb : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private int _specificationValue;
    [SerializeField] private Sprite _icon;
    [SerializeField] private LimbType _type;
    [SerializeField] private int _health;

    private readonly string _selectText = "Select";
    private int _isBuiedInt => PlayerPrefs.GetInt(_label, 0);
    private int _isSelectInt => PlayerPrefs.GetInt(_label + _selectText, 0);

    public string Label => _label;
    public int Price => _price;
    public int SpecificationValue => _specificationValue;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuiedInt == 1;
    public bool IsSelect => _isSelectInt == 1;
    public LimbType Type => _type;
    public int Health => _health;

    public void Buy()
    {
        PlayerPrefs.SetInt(_label, 1);
    }

    public void Selecte()
    {
        PlayerPrefs.SetInt(_label + _selectText, 1);
    }

    public void Unselecte()
    {
        PlayerPrefs.SetInt(_label + _selectText, 0);
    }
}