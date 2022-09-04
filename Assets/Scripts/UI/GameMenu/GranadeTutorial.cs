using UnityEngine;
using System.Collections.Generic;

public class GranadeTutorial : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private LaunchGrenadeGun _launchGrenadeGun;
    [SerializeField] private Player _player;
    [SerializeField] private TapGrenadeTutorialTarget _tapGrenadeTutorialTarget;
    [SerializeField] private List<EnemySpawner> _enemySpawners;

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
        _playerWeaponsHolder.StopWeaponShooting();
        _launchGrenadeGun.Game.StartGrenadeTutorial();
        _playerMover.StartTutorialMove();

        for (int i = 0; i < _enemySpawners.Count; i++)
            _enemySpawners[i].StopEnemies();
    }

    private void OnTutorialEnded()
    {
        _player.StartAnimation();
        _launchGrenadeGun.Game.EndGrenadeTutorial();
        _playerMover.EndTutorialMove();
        _playerWeaponsHolder.StartWeaponShooting();

        for (int i = 0; i < _enemySpawners.Count; i++)
            _enemySpawners[i].ContinueEnemiesAttack();
    }
}