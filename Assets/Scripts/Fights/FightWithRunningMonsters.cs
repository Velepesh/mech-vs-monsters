using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FightWithRunningMonsters : MonoBehaviour
{
    [SerializeField] private List<RunningMonster> _monsters;
    [SerializeField] private Game _game;
    [SerializeField] private Transform _targetPlayerPosition;
    [SerializeField] private float _delayTime;
    [SerializeField] private FightType _fightType;

    private Player _player;

    private void OnEnable()
    {
        for (int i = 0; i < _monsters.Count; i++)
            _monsters[i].Disabled += OnMonsterDisabled;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _monsters.Count; i++)
            _monsters[i].Disabled -= OnMonsterDisabled;
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
        for (int i = 0; i < _monsters.Count; i++)
            _monsters[i].Win();
        
        damageable.Died -= OnPlayerDied;
    }

    private IEnumerator StartFight(Player player, float delayTime)
    {
        player.PrepearedForFight(_targetPlayerPosition, _monsters[1], _fightType);

        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < _monsters.Count; i++)
            _monsters[i].Fight(player);

        player.Fight(_monsters[1]);
        player.Died += OnPlayerDied;
    }

    private void OnMonsterDisabled(Monster monster)
    {
        if(_monsters.Count > 0)
        {
            if(monster is RunningMonster runningMonster)
                _monsters.Remove(runningMonster);

            if(_monsters.Count == 0 && _player.IsDied == false)
            {
                _player.WinInFight();
                _game.WinInBattle();
            }
        }       
    }
}