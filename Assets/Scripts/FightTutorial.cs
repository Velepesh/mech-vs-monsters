using UnityEngine;

public class FightTutorial : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _tutorialPanel;

    private void Start()
    {
        DisableTutorialPanel();
    }

    private void OnEnable()
    {
        _game.Fought += OnFought;
        _game.BattleWon += OnBattleWon;
    }

    private void OnDisable()
    {
        _game.Fought -= OnFought;
        _game.BattleWon -= OnBattleWon;
    }

    private void OnFought()
    {
        EnableTutorialPanel();
    }
    private void OnBattleWon()
    {
        DisableTutorialPanel();
    }

    private void EnableTutorialPanel()
    {
        _tutorialPanel.gameObject.SetActive(true);
    }

    private void DisableTutorialPanel()
    {
        _tutorialPanel.gameObject.SetActive(false);
    }
}