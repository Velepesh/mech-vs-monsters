using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private CinemachineVirtualCamera _menuCamera;
    [SerializeField] private CinemachineVirtualCamera _gameCamera;
    [SerializeField] private CinemachineVirtualCamera _fightCamera;
    [SerializeField] private PlayableDirector _menuPlayableDirector;
    [SerializeField] private PlayableDirector _fightPlayableDirector;

    private FightType _fightType;

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
        _game.Fought += OnFought;
        _game.FoughtWon += OnBattleWon;

        CameraSwitcher.Register(_fightCamera);
        CameraSwitcher.Register(_gameCamera);
        CameraSwitcher.Register(_menuCamera);
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
        _game.Fought -= OnFought;
        _game.FoughtWon -= OnBattleWon;

        CameraSwitcher.Unregister(_menuCamera);
        CameraSwitcher.Unregister(_gameCamera);
        CameraSwitcher.Unregister(_fightCamera);
    }

    private void Start()
    {
        _menuCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0;
        CameraSwitcher.SwitchCamera(_menuCamera);
    }

    private void OnLevelStarted()
    {
        _menuPlayableDirector.Play();
        StartCoroutine(SwitchCamera(_gameCamera, (float)_menuPlayableDirector.duration));
    }

    private void OnFought(FightType type)
    {
        if (type == FightType.Hands)
        {
            CameraSwitcher.SwitchCamera(_fightCamera);
            _fightCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 2;
        }

        _fightType = type;
    }

    private void OnBattleWon()
    {
        if (_fightType == FightType.Hands)
        {
            _fightPlayableDirector.Play();
            StartCoroutine(SwitchCamera(_gameCamera, (float)_fightPlayableDirector.duration));
        }
    }

    private IEnumerator SwitchCamera(CinemachineVirtualCamera camera, float time)
    {
        yield return new WaitForSeconds(time);

        CameraSwitcher.SwitchCamera(camera);
    }
}
