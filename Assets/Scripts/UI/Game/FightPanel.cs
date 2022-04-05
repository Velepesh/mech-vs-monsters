using UnityEngine;
using UnityEngine.UI;

public class FightPanel : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Button _rocketLauncherButton;
    [SerializeField] private GameObject _tutorialPanel;

    private void Start()
    {
        DisableFightPanel();
    }

    private void OnEnable()
    {
        _game.Fought += OnFought;
        _game.FoughtWon += OnBattleWon;
    }

    private void OnDisable()
    {
        _game.Fought -= OnFought;
        _game.FoughtWon -= OnBattleWon;
    }

    private void OnFought()
    {
        EnableFightPanel();
    }

    private void OnBattleWon()
    {
        DisableFightPanel();
    }

    private void EnableFightPanel()
    {
        _rocketLauncherButton.gameObject.SetActive(false);
        _tutorialPanel.gameObject.SetActive(true);
    }

    private void DisableFightPanel()
    {
        _tutorialPanel.gameObject.SetActive(false);
    }
}