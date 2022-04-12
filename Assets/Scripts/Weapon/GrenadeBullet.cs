using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class GrenadeBullet : DamageCollider
{
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _showButtonDistance;
    [SerializeField] private float _stopButtonRotationDistance;
    [SerializeField] private float _tutorialDistance;

    private readonly float _stopSpeed = 0f;
    
    private float _startSpeed;
    private Player _player;
    private bool _isTutorial;

    public bool IsTutorial => _isTutorial;

    public event UnityAction ButtonShowed;
    public event UnityAction TutorialShowed;
    public event UnityAction TutorialEnded;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
    }

    private void Start()
    {
        _startSpeed = _speed;
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
        _player.Fought -= OnFought;
        _player.Prepeared -= OnPrepeared;
    }

    public void Init(Player player, bool isTutorial)
    {
        _isTutorial = isTutorial;
        _player = player;
        _player.Died += OnDied;
        _player.Fought += OnFought;
        _player.Prepeared += OnPrepeared;
    }

    public void Explosion()
    {
        GameObject explosion = Instantiate(_impactPrefab, transform.position, _impactPrefab.transform.rotation);
        Destroy();
    }

    public void StartGrenade()
    {
        _speed = _startSpeed;
        TutorialEnded?.Invoke();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            DoDamage(damageable);

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);

        Vector3 pos = contact.point;
        Instantiate(_impactPrefab, pos, rot);
        Destroy();
    }

    private void Move()
    {
        if (_player.IsDied == false)
        {
            MoveToTarget();

            TryShowButton();
        }
    }

    private void MoveToTarget()
    {
        float distance = Vector3.Distance(transform.position, _player.Position);

        if (distance >= _stopButtonRotationDistance)
            transform.LookAt(_player.Position);

        if (distance <= _tutorialDistance && _isTutorial)
        {
            TutorialShowed?.Invoke();
            StopGrenade();
        }
            
        transform.position = Vector3.MoveTowards(transform.position, _player.Position + _offset, _speed * Time.deltaTime);
    }


    private void TryShowButton()
    {
        float distance = Vector3.Distance(transform.position, _player.Position);

        if (distance <= _showButtonDistance)
            ButtonShowed?.Invoke();
    }

    private void OnDied(IDamageable damageable)
    {
        Explosion();
    }

    private void OnFought(Monster monster)
    {
        Explosion();
    }

    private void OnPrepeared(Transform transform, Monster monster, bool isAiming)
    {
        Explosion();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void StopGrenade()
    {
        _speed = _stopSpeed;

        TutorialShowed?.Invoke();
    }
}