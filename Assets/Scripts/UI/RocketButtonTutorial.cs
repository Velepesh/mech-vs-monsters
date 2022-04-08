using UnityEngine;
using UnityEngine.UI;
public class RocketButtonTutorial : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private Button _rocketLauncherButton;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerWeaponsHolder _playerWeaponsHolder;
    [SerializeField] private Machinegun _tankGun;

    private void OnEnable()
    {
        _rocketLauncher.Shooted += OnShooted;
    }

    private void OnDisable()
    {
        _rocketLauncher.Shooted -= OnShooted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _game.StartTutorial();
            _playerMover.StartTutorialMove();
            _playerWeaponsHolder.StopShooting();
            _tankGun.StopShooting();
            _rocketLauncherButton.gameObject.SetActive(true);
            _animator.SetTrigger(AnimatorRocketTutorialController.States.StartTutorial);
        }
    }

    private void OnShooted()
    {
        _game.EndTutorial();
        _playerMover.EndTutorialMove();
        _playerWeaponsHolder.StartShooting();
        
        if(_tankGun != null)
            _tankGun.StartShooting();
        
        _animator.SetTrigger(AnimatorRocketTutorialController.States.EndTutorial);
    }
}
