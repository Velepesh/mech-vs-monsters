using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Battle : MonoBehaviour
{
    [SerializeField] private Godzilla _godzilla;
    [SerializeField] private GameObject _godzillaColliderBody;
    [SerializeField] private Game _game;
    [SerializeField] private Transform _targetPlayerPosition;
    [SerializeField] private float _delayTime;

    private Player _player;
    private void OnEnable()
    {
        _godzilla.Died += OnGodzillaDied;
    }

    private void OnDisable()
    {
        _godzilla.Died -= OnGodzillaDied;
    }

    private void Awake()
    {
        _godzillaColliderBody.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            _game.Fight();
            StartCoroutine(StartFight(player, _delayTime));
        }
    }

    private void OnPlayerDied(IDamageable damageable)
    {
        _godzilla.Win();
        damageable.Died -= OnPlayerDied;
    }

    private IEnumerator StartFight(Player player, float time)
    {
        player.PrepearedForFight(_targetPlayerPosition, _godzilla);
        yield return new WaitForSeconds(time);

        player.Fight(_targetPlayerPosition, _godzilla);
        _godzilla.Fight();
        player.Stand();
        player.Died += OnPlayerDied;
        _godzillaColliderBody.SetActive(true);
    }

    private void OnGodzillaDied(IDamageable damageable)
    {
        _player.StartMove();
        _game.WinInBattle();
        damageable.Died -= OnGodzillaDied;
        Destroy(this);
    }
}