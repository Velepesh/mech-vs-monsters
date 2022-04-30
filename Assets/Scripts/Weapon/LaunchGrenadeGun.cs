using UnityEngine;
using UnityEngine.Events;

public class LaunchGrenadeGun : Weapon, IShooteable
{
    [SerializeField] private Game _game;
    [SerializeField] private Player _player;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private GrenadeBullet _bullet;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private bool _isTutorial = false;

    private float _shootingTimer;
    private bool _canShoot = true;
    private GrenadeBullet _tutorialBullet;

    public Game Game => _game;
    public GrenadeBullet TutorialBullet => _tutorialBullet;

    public override event UnityAction Shooted;
    public event UnityAction TutorialShowed;
    public event UnityAction TutorialEnded;

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
        _player.Fought += OnFought;
        _player.Prepeared += OnPrepeared;
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
        _player.Fought -= OnFought;
        _player.Prepeared -= OnPrepeared;
        _player.Died -= OnDied;
    }

    private void Update()
    {
        if (IsShooting)
        {
            if (_player.IsDied || _canShoot == false)
            {
                StopShooting();
            }
            else
            {
                if (_shootingTimer >= _cooldownTime)
                {
                    Shoot();
                    _shootingTimer = 0;
                }

                _shootingTimer += Time.deltaTime;
            }
        }
    }

    public new void Shoot()
    {
        int index = Random.Range(0, _spawnPoints.Length);

        GameObject bullet = Instantiate(_bullet.gameObject, _spawnPoints[index].position, _spawnPoints[index].rotation);
        GrenadeBullet grenadeBullet = bullet.GetComponent<GrenadeBullet>();

        grenadeBullet.Init(_player, _isTutorial);

        if (_isTutorial)
        {
            _canShoot = false;
            _isTutorial = false;
            _tutorialBullet = grenadeBullet;
            _tutorialBullet.TutorialShowed += OnTutorialShowed;
            _tutorialBullet.TutorialEnded += OnTutorialEnded;
        }

        Shooted?.Invoke();
    }

    private void OnLevelStarted()
    {
        StartShooting();
    }
    
    private void OnFought(Monster monster)
    {
        StopShooting();
    }

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
    {
        StopShooting();
    }

    private void OnDied(IDamageable damageable)
    {
        StopShooting();
    }

    private void OnTutorialShowed()
    {
        TutorialShowed?.Invoke();
        _tutorialBullet.TutorialShowed -= OnTutorialShowed;
    } 
    
    private void OnTutorialEnded()
    {
        _canShoot = true;
        TutorialEnded?.Invoke();
        _tutorialBullet.TutorialEnded -= OnTutorialEnded;
    }
}