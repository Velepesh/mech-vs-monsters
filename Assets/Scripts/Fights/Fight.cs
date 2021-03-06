using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class Fight : MonoBehaviour
{
    [SerializeField] private Monster _monster;
    [SerializeField] private Game _game;
    [SerializeField] private Transform _targetPlayerPosition;
    [SerializeField] private float _delayTime;
    [SerializeField] private FightType _fightType;
    [SerializeField] private GameObject _borderCollider;

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
            _game.Fight(_fightType);
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
        player.PrepearedForFight(_targetPlayerPosition, _monster, _fightType);

        if (_monster is Ahriman ahriman)
            ahriman.PrepearedForFight();

        yield return new WaitForSeconds(delayTime);

        if (_monster is Ahriman monster)
            monster.Fight(player);
        else
            _monster.Fight(player);

        player.Fight(_monster);
        player.Died += OnPlayerDied;
    }

    private void OnMonsterDisabled(Monster monster)
    {
        if (_player.IsDied == false)
        {
            _player.WinInFight();
            _game.WinInBattle();
            
            if(_fightType == FightType.Hands == false)
                _borderCollider.SetActive(false);
        }
    }
}