using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private SceneChanger _sceneChanger;
    [SerializeField] private AmplitudeAnalytics _amplitudeAnalytics;

    private void Start()
    {
        _amplitudeAnalytics.GameStart();
        _sceneChanger.LoadLevel(_game.CurrentLevel);
    }
}