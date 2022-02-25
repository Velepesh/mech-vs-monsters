using UnityEngine;

public class LightDisabler : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Light _light;

    private void Awake()
    {
        _light.enabled = true;
    }
    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        _light.enabled = false;
    }
}