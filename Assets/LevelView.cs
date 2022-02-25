using UnityEngine;
using TMPro;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private TMP_Text _levelText;

    private readonly string _level = "Level ";

    private void Start()
    {
        _levelText.text = _level + _game.CurrentLevel.ToString();
    }
}