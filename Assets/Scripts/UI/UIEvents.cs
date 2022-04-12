using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private UnityEvent _onLevelStarted;
    [SerializeField] private UnityEvent _onGameWon;
    [SerializeField] private UnityEvent _onGameLost;
    [SerializeField] private UnityEvent _onRocketTutorialStarted;
    [SerializeField] private UnityEvent _onRocketTutorialEnded;
    [SerializeField] private UnityEvent _onGrenadeTutorialStarted;
    [SerializeField] private UnityEvent _onGrenadeTutorialEnded;
    [SerializeField] private UnityEvent _onAimTutorialStarted;
    [SerializeField] private UnityEvent _onAimTutorialEnded;
    [SerializeField] private float _delayTimeBeforeWinPanel;

    private void OnValidate()
    {
        _delayTimeBeforeWinPanel = Mathf.Clamp(_delayTimeBeforeWinPanel, 0, float.MaxValue);
    }

    private void OnEnable()
    {
        _game.LevelStarted += () => _onLevelStarted?.Invoke();
        _game.LevelWon += OnLevelWon;
        _game.LevelLost += () => _onGameLost?.Invoke();
        _game.RocketTutorialStarted += () => _onRocketTutorialStarted?.Invoke();
        _game.RocketTutorialEnded += () => _onRocketTutorialEnded?.Invoke();
        _game.GrenadeTutorialStarted += () => _onGrenadeTutorialStarted?.Invoke();
        _game.GrenadeTutorialEnded += () => _onGrenadeTutorialEnded?.Invoke();
        _game.AimTutorialStarted += () => _onAimTutorialStarted?.Invoke();
        _game.AimTutorialEnded += () => _onAimTutorialEnded?.Invoke();

        _game.MainMenu();
    }

    private void OnDisable()
    {
        _game.LevelStarted -= () => _onLevelStarted?.Invoke();
        _game.LevelWon -= OnLevelWon;
        _game.LevelLost -= () => _onGameLost?.Invoke();
        _game.RocketTutorialStarted -= () => _onRocketTutorialStarted?.Invoke();
        _game.RocketTutorialEnded -= () => _onRocketTutorialEnded?.Invoke();
        _game.GrenadeTutorialStarted -= () => _onGrenadeTutorialStarted?.Invoke();
        _game.GrenadeTutorialEnded -= () => _onGrenadeTutorialEnded?.Invoke();
    }

    private void OnLevelWon()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delayTimeBeforeWinPanel);

        _onGameWon?.Invoke();
    }
}