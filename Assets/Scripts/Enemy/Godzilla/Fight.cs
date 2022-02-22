using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Fight : MonoBehaviour
{
    [SerializeField] private Godzilla _godzilla;
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

    private IEnumerator StartFight(Player player, float delayTim)
    {
        player.PrepearedForFight(_targetPlayerPosition, _godzilla);
        yield return new WaitForSeconds(delayTim);

        _godzilla.Fight();
        player.Fight(_godzilla);
        player.Died += OnPlayerDied;
    }

    private void OnGodzillaDied(IDamageable damageable)
    {
        _player.StartMove();
        _game.WinInBattle();
        damageable.Died -= OnGodzillaDied;
        gameObject.SetActive(false);
    }
}