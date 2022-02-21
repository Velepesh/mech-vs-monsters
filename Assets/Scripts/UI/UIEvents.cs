using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UIEvents : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private UnityEvent _onLevelStarted;
    [SerializeField] private UnityEvent _onGameWon;
    [SerializeField] private UnityEvent _onGameLost;
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

        _game.MainMenu();
    }

    private void OnDisable()
    {
        _game.LevelStarted -= () => _onLevelStarted?.Invoke();
        _game.LevelWon -= OnLevelWon;
        _game.LevelLost -= () => _onGameLost?.Invoke();
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