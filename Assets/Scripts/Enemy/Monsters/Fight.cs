using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Fight : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Game _game;
    [SerializeField] private Transform _targetPlayerPosition;
    [SerializeField] private float _delayTime;

    private Player _player;

    private void OnEnable()
    {
        _monster.Disabled += OnMonsterDisabled;
    }

    private void OnDisable()
    {
        _monster.Disabled -= OnMonsterDisabled;
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
        _monster.Win();
        damageable.Died -= OnPlayerDied;
    }

    private IEnumerator StartFight(Player player, float delayTime)
    {
        player.PrepearedForFight(_targetPlayerPosition, _monster);
        yield return new WaitForSeconds(delayTime);

        _monster.Fight();
        player.Fight(_monster);
        player.Died += OnPlayerDied;
    }

    private void OnMonsterDisabled()
    {
        if (_player.IsDied == false)
        {
            _player.StartMove();
            _game.WinInBattle();
        }

       // damageable.Died -= OnMonsterDied;
      //  Destroy(gameObject);
    }
}