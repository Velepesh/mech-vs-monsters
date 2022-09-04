using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class GrenadeBullet : AttackCollider, IDamageable
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _showButtonDistance;
    [SerializeField] private float _stopButtonRotationDistance;
    [SerializeField] private float _tutorialDistance;

    private readonly float _stopSpeed = 0f;
    
    private Player _player;
    private bool _isTutorial;
    private bool _isStopped;

    public bool IsTutorial => _isTutorial;
    public Health Health => _health;
    public bool IsDied => _health.Value <= 0;

    public event UnityAction ButtonShowed;
    public event UnityAction TutorialShowed;
    public event UnityAction TutorialEnded;
    public event UnityAction<IDamageable> Died;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
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

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            Attack(damageable);
            Explosion();
        }
    }

    private void EndTutorial()
    {
        _isTutorial = false;
        TutorialEnded?.Invoke();
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

        if (distance <= _tutorialDistance && _isTutorial && _isStopped == false)
            StopGrenade();
            
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

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
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
        _isStopped = true;

        TutorialShowed?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
       
        if (IsDied)
            Die();
    }

    public void Die()
    {
        if (_isTutorial)
            EndTutorial();

        Explosion();
        Died?.Invoke(this);
    }
}