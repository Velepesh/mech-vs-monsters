using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Player _player;
    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);

    public event UnityAction LevelStarted;
    public event UnityAction LevelWon;
    public event UnityAction LevelLost;
    public event UnityAction LevelRestart;
    public event UnityAction LevelMainMenu;
    public event UnityAction Fought;
    public event UnityAction BattleWon;

    private void OnEnable()
    {
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void Fight()
    {
        Fought?.Invoke();
    }

    public void WinInBattle()
    {
        BattleWon?.Invoke();
    }

    public void MainMenu()
    {
        LevelMainMenu?.Invoke();
    }

    public void NextLevel()
    {
        _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void RestartLevel()
    {
        LevelRestart?.Invoke();
        _sceneChanger.LoadLevel(CurrentLevel);
    }

    public void StartLevel()
    {
        _player.StartLevel();

        LevelStarted?.Invoke();
    }

    public void WinGame()
    {
        LevelWon?.Invoke();
        PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);
    }

    public void LoseGame()
    {
        LevelLost?.Invoke();
    }

    private void OnDied(IDamageable damageable)
    {
        LoseGame();
    }
}