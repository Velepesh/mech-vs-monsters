using UnityEngine;

public class MonsterDamagedEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damagedEffect;
    [SerializeField] private Transform _spawnPoint;

    private TimerMonsterCollider _timerMonsterCollider;

    private void Awake()
    {
        _timerMonsterCollider = GetComponent<TimerMonsterCollider>();
    }

    private void OnEnable()
    {
        _timerMonsterCollider.TimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        _timerMonsterCollider.TimerEnded -= OnTimerEnded;
    }

    private void OnTimerEnded(TimerMonsterCollider timerMonsterCollider)
    {
        PlayEffect();
    }

    private void PlayEffect()
    {
        Instantiate(_damagedEffect.gameObject, _spawnPoint);
    }
}
