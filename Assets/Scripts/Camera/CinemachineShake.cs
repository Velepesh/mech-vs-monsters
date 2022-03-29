using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using System.Collections;

public class CinemachineShake : MonoBehaviour 
{
    [SerializeField] private Player _player;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private DownMover _downMover;
    [SerializeField] private PlayerWeaponsHolder _playerWeapons;
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private float _highIntensity;
    [SerializeField] private float _longTime;
    [SerializeField] private float _lowIntensity;
    [SerializeField] private float _shortTime;

    private List<Weapon> _weapons = new List<Weapon>();

    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private bool _isRocket;

    private void OnValidate()
    {
        _highIntensity = Mathf.Clamp(_highIntensity, 0f, float.MaxValue);
        _longTime = Mathf.Clamp(_longTime, 0f, float.MaxValue);
        _lowIntensity = Mathf.Clamp(_lowIntensity, 0f, float.MaxValue);
        _shortTime = Mathf.Clamp(_shortTime, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _downMover.Landed += OnLanded;

        InitGuns();
    }

    private void InitGuns()
    {
        for (int i = 0; i < _playerWeapons.Count; i++)
        {
            Weapon weapon = _playerWeapons.GetWeapon(i);

            _weapons.Add(weapon);
            weapon.Shooted += OnShooted;
        }

        _rocketLauncher.Shooted += OnRocketLauncherShooted;
        _player.DamageTook += OnDamageTook;
    }

    private void OnDisable()
    {
        _rocketLauncher.Shooted -= OnRocketLauncherShooted;
        _downMover.Landed -= OnLanded;

        for (int i = 0; i < _weapons.Count; i++)
            _weapons[i].Shooted -= OnShooted;

        _player.DamageTook -= OnDamageTook;
    }

    private void OnDamageTook()
    {
        ShakeHightIntencity();
    }

    private void OnRocketLauncherShooted()
    {
        _isRocket = true;
        ShakeHightIntencity();
    }

    private void ShakeHightIntencity()
    {
        ShakeCamera(_highIntensity, _longTime);
        StartCoroutine(WaitRocketLauncherShaking(_longTime));
    }

    private IEnumerator WaitRocketLauncherShaking(float time)
    {
        yield return new WaitForSeconds(time);

        _isRocket = false;
    }

    private void OnShooted()
    {
        if(_isRocket == false)
            ShakeCamera(_lowIntensity, _shortTime);
    }

    private void OnLanded()
    {
        ShakeCamera(_highIntensity, _longTime);
    }

    private void ShakeCamera(float intensity, float time) 
    {
        _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        _startingIntensity = intensity;
        _shakeTimerTotal = time;
        _shakeTimer = time;
    }

    private void Update() 
    {
        if (_shakeTimer > 0) 
        {
            _shakeTimer -= Time.deltaTime;

            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 
                Mathf.Lerp(_startingIntensity, 0f, 1 - _shakeTimer / _shakeTimerTotal);
        }
    }
}