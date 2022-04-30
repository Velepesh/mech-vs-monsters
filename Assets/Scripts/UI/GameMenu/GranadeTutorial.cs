using UnityEngine;

public class GranadeTutorial : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private LaunchGrenadeGun _launchGrenadeGun;
    [SerializeField] private Player _player;
    [SerializeField] private TapGrenadeTutorialTarget _tapGrenadeTutorialTarget;
    [SerializeField] private GameObject _rocketgunButton;

    private void OnEnable()
    {
        _launchGrenadeGun.TutorialShowed += OnTutorialShowed;
        _launchGrenadeGun.TutorialEnded += OnTutorialEnded;
    }

    private void OnDisable()
    {
        _launchGrenadeGun.TutorialShowed -= OnTutorialShowed;
        _launchGrenadeGun.TutorialEnded -= OnTutorialEnded;
    }

    private void OnTutorialShowed()
    {
        _tapGrenadeTutorialTarget.SetTarget(_launchGrenadeGun.TutorialBullet.transform);
        _player.StopAnimation();
        _playerWeaponsHolder.StopMainWeaponShooting();
        _launchGrenadeGun.Game.StartGrenadeTutorial();
        _playerMover.StartTutorialMove();
        _rocketgunButton.SetActive(false);
    }

    private void OnTutorialEnded()
    {
        _player.StartAnimation();
        _launchGrenadeGun.Game.EndGrenadeTutorial();
        _playerMover.EndTutorialMove();
        _playerWeaponsHolder.StartMainWeaponShooting();
        _rocketgunButton.SetActive(true);
    }
}