using UnityEngine;

[CreateAssetMenu(fileName = "AdditionalWeapon", menuName = "AdditionalWeapon/Weapon", order = 51)]
public class AdditionalWeapon : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _icon;

    private int _isBuiedInt => PlayerPrefs.GetInt(Label, 0);
    public string Label => _label;
    public int Price => _price;
    public int Level => _level;
    public Sprite Icon => _icon;
    public bool IsBuyed => _isBuiedInt == 1;

    public void Buy()
    {
        PlayerPrefs.SetInt(Label, 1);
    }
}