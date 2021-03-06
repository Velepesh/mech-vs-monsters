using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private Player _player;
    
    private const string CURRENT_LEVEL_ID = "CurrentLevelID";

    private float _spentTime = 0f;
    private bool _isPlaying = false;
    public int CurrentLevel => PlayerPrefs.GetInt(CURRENT_LEVEL_ID, 1);
    public int SpentTime => (int)_spentTime;

    public event UnityAction LevelStarted;
    public event UnityAction LevelWon;
    public event UnityAction LevelLost;
    public event UnityAction LevelRestart;
    public event UnityAction LevelMainMenu;
    public event UnityAction<FightType> Fought;
    public event UnityAction FoughtWon;
    public event UnityAction RocketTutorialStarted;
    public event UnityAction RocketTutorialEnded;
    public event UnityAction GrenadeTutorialStarted;
    public event UnityAction GrenadeTutorialEnded;
    public event UnityAction AimTutorialStarted;
    public event UnityAction AimTutorialEnded;

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

    private void Update()
    {
        if (_isPlaying)
            _spentTime += Time.deltaTime;
    }

    public void Fight(FightType type)
    {
        Fought?.Invoke(type);
    }

    public void WinInBattle()
    {
        FoughtWon?.Invoke();
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
        _isPlaying = true;
        _player.StartLevel();

        LevelStarted?.Invoke();
    }

    public void WinGame()
    {
        _isPlaying = false;
        LevelWon?.Invoke();
        PlayerPrefs.SetInt(CURRENT_LEVEL_ID, CurrentLevel + 1);
    }

    public void LoseGame()
    {
        _isPlaying = false;
        LevelLost?.Invoke();
    }

    private void OnDied(IDamageable damageable)
    {
        LoseGame();
    }

    public void StartRocketTutorial()
    {
        Time.timeScale = 0.5f;
        RocketTutorialStarted?.Invoke();
    }

    public void EndRocketTutorial()
    {
        Time.timeScale = 1f;
        RocketTutorialEnded?.Invoke();
    }

    public void StartGrenadeTutorial()
    {
        Time.timeScale = 0.5f;
        GrenadeTutorialStarted?.Invoke();
    }

    public void EndGrenadeTutorial()
    {
        Time.timeScale = 1f;
        GrenadeTutorialEnded?.Invoke();
    }

    public void StartAimTutorial()
    {
        AimTutorialStarted?.Invoke();
    }

    public void EndAimTutorial()
    {
        AimTutorialEnded?.Invoke();
    }
}