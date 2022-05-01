using UnityEngine;

[RequireComponent(typeof(Player))]
public class HealingEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _healingEffect;
    [SerializeField] private Transform _spawnPoint;

    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Health.Healed += OnHealed;
    }

    private void OnDisable()
    {
        _player.Health.Healed -= OnHealed;
    } 
    
    private void OnHealed()
    {
        GameObject effect = Instantiate(_healingEffect.gameObject, _spawnPoint);
    }
}