using UnityEngine;
using UnityEngine.UI;

public class AimTutorial : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private DownMover _downMover;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private Button _rocketLauncherButton;
    [SerializeField] private AimShooting _aimShooting;

    private void OnEnable()
    {
        _downMover.Landed += OnLanded;
        _aimShooting.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _downMover.Landed -= OnLanded;
        _aimShooting.Shooted -= OnShooted;
    }

    private void OnLanded()
    {
        _game.StartAimTutorial();
        _player.StopAnimation();
        _playerMover.StartTutorialMove();
        _playerWeaponsHolder.StopWeaponShooting();
        _rocketLauncherButton.gameObject.SetActive(false);
    }

    private void OnShooted()
    {
        _game.EndAimTutorial();
        _player.StartAnimation();
        _playerMover.EndTutorialMove();
        _playerWeaponsHolder.StartWeaponShooting();
        _rocketLauncherButton.gameObject.SetActive(true);
    }
}