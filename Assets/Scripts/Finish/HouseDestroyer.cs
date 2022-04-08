using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class HouseDestroyer : MonoBehaviour
{
    [SerializeField] private List<House> _houses;
    [SerializeField] private float _time;
    [SerializeField] private Slider _slider;
    [SerializeField] private Game _game;
    [SerializeField] private Button _rocketLauncherButton;

    readonly private int _secondLevel = 2;

    private bool _isTimer;
    private float _timeLeft;
    private Player _player;

    private void OnEnable()
    {
        for (int i = 0; i < _houses.Count; i++)
        {
            _houses[i].Died += OnDied;
            _houses[i].GetComponent<Collider>().enabled = false;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _houses.Count; i++)
            _houses[i].Died -= OnDied;
    }
    private void Start()
    {
        _slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_isTimer)
        {
            if (_timeLeft > 0f)
            {
                _timeLeft -= Time.deltaTime;
                _slider.value = _timeLeft / _time;
            }
            else
            {
                StopTimer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _player = player;
            StartTimer();

            if(_game.CurrentLevel > _secondLevel)
                _rocketLauncherButton.gameObject.SetActive(true);
        }
    }

    private void OnDied(IDamageable damageable)
    {
        _houses.Remove((House)damageable);

        EnableNextHouseCollider();
    }

    private void StartTimer()
    {
        _isTimer = true;
        _slider.gameObject.SetActive(true);
        _slider.value = _time;
        _timeLeft = _time;

        EnableNextHouseCollider();
    }

    private void EnableNextHouseCollider()
    {
        if (_houses.Count > 0)
        {
            House house = GetClosestTarget(_player.Position, _houses);

            house.GetComponent<Collider>().enabled = true;
        }
    }

    private void StopTimer()
    {
        for (int i = 0; i < _houses.Count; i++)
        {
            _houses[i].Died -= OnDied;
            _houses[i].GetComponent<Collider>().enabled = false;
        }

        _player.Win();
        _game.WinGame();
        _isTimer = false;
        _slider.gameObject.SetActive(false);
    }

    private House GetClosestTarget(Vector3 weaponPosition, List<House> damageables)
    {
        House closest = null;
        float maxRange = float.MaxValue;

        foreach (House target in damageables)
        {
            float distance = Vector3.Distance(target.Position, weaponPosition);

            if (distance < maxRange)
            {
                maxRange = distance;
                closest = target;
            }
        }

        return closest;
    }
}