using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PowerWeapon : Weapon, IShooteable
{
    [SerializeField] private Godzilla _godzilla;
    [SerializeField] private ParticleSystem _fireEffect;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldownTime;
    
    private Collider _attackZone;

    public override event UnityAction Shooted;

    private void OnValidate()
    {
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
    }

    private void Awake()
    {
        _attackZone = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _godzilla.Attacked += OnAttacked;
        _godzilla.AttackStopped += OnAttackStopped;
    }

    private void OnDisable()
    {
        _godzilla.Attacked -= OnAttacked;
        _godzilla.AttackStopped -= OnAttackStopped;
    }

    public new void Shoot()
    {
        _fireEffect.Play();
        _attackZone.enabled = true;

        Shooted?.Invoke();
    }

    private void OnAttacked()
    {
        Shoot();
    }

    private void OnAttackStopped()
    {
        _fireEffect.Stop();
        _attackZone.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            StartCoroutine(Reload(_cooldownTime, player));
    }

    private IEnumerator Reload(float time, Player player)
    {
        _attackZone.enabled = true;
        player.TakeDamage(_damage);

        yield return new WaitForSeconds(time);
        
        _attackZone.enabled = false;
    }
}