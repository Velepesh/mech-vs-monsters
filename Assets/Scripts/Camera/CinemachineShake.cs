using UnityEngine;
using System.Collections.Generic;
using Cinemachine;
using System.Collections;

public class CinemachineShake : MonoBehaviour 
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private PlayerWeaponsHolder _playerWeapons;
    [SerializeField] private RocketLauncher _rocketLauncher;
    [SerializeField] private float _rocketLauncherIntensity;
    [SerializeField] private float _rocketLauncherTime;
    [SerializeField] private float _machinegunIntensity;
    [SerializeField] private float _machinegunTime;

    private List<Weapon> _weapons = new List<Weapon>();

    private float _shakeTimer;
    private float _shakeTimerTotal;
    private float _startingIntensity;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private bool _isRocket;

    private void OnValidate()
    {
        _rocketLauncherIntensity = Mathf.Clamp(_rocketLauncherIntensity, 0f, float.MaxValue);
        _rocketLauncherTime = Mathf.Clamp(_rocketLauncherTime, 0f, float.MaxValue);
        _machinegunIntensity = Mathf.Clamp(_machinegunIntensity, 0f, float.MaxValue);
        _machinegunTime = Mathf.Clamp(_machinegunTime, 0f, float.MaxValue);
    }

    private void OnEnable()
    {
        _cinemachineBasicMultiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

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
    }

    private void OnDisable()
    {
        _rocketLauncher.Shooted -= OnRocketLauncherShooted;

        for(int i = 0; i < _weapons.Count; i++)
            _weapons[i].Shooted -= OnShooted;
    }

    private void OnRocketLauncherShooted()
    {
        _isRocket = true;
        ShakeCamera(_rocketLauncherIntensity, _rocketLauncherTime);
        StartCoroutine(WaitRocketLauncherShaking(_rocketLauncherTime));
    }

    private IEnumerator WaitRocketLauncherShaking(float time)
    {
        yield return new WaitForSeconds(time);

        _isRocket = false;
    }

    private void OnShooted()
    {
        if(_isRocket == false)
            ShakeCamera(_machinegunIntensity, _machinegunTime);
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