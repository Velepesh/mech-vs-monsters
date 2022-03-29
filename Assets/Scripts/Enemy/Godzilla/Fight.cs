using UnityEngine;
using System.Collections;
using Cinemachine;
using AnimatedUI;

[RequireComponent(typeof(Collider))]
public class Fight : MonoBehaviour
{
    [SerializeField] private Godzilla _godzilla;
    [SerializeField] private Game _game;
    [SerializeField] private Transform _targetPlayerPosition;
    [SerializeField] private float _delayTime;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private GameObject _fightPanel;
    [SerializeField] private CanvasFade _winPanel;

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

    private IEnumerator StartFight(Player player, float delayTime)
    {
        player.PrepearedForFight(_targetPlayerPosition, _godzilla);
        yield return new WaitForSeconds(delayTime);

        _godzilla.Fight();
        player.Fight(_godzilla);
        player.Died += OnPlayerDied;
    }

    private void OnGodzillaDied(IDamageable damageable)
    {
        if (_player.IsDied == false)
        {
            _camera.m_Priority = 10;
            _winPanel.Show();
            _fightPanel.SetActive(false);

            _player.Win();
           // _game.WinInBattle();
        }

        damageable.Died -= OnGodzillaDied;
        Destroy(gameObject);
    }
}