using UnityEngine;

[CreateAssetMenu(fileName = "AdditionalWeapon", menuName = "AdditionalWeapon/Weapon", order = 51)]
public class AdditionalWeapon : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _isBuyed = false;

    public string Label => _label;
    public int Price => _price;
    public int Level => _level;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuyed;

    public void Buy()
    {
        _isBuyed = true;
    }
}