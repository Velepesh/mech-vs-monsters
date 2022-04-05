using UnityEngine;
using TMPro;

public class HouseView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private House _house;

    private void Start()
    {
        _text.text = _house.Award.ToString();
    }
}