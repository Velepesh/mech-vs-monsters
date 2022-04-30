using System.Collections.Generic;
using UnityEngine;

public class LookAtIK : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<GameObject> _hands;
    [SerializeField] private RobotBuilder _robotBuilder;

    private Vector3 _targetPosition;

    private void Start()
    {
        for (int i = 0; i < _hands.Count; i++)
            _hands[i].SetActive(false);
    }

    private void OnEnable()
    {
        _player.FightWon += OnFightWon;
        _player.Died += OnDied;
        _player.Prepeared += OnPrepeared;
    }

    private void OnDisable()
    {
        _player.FightWon -= OnFightWon;
        _player.Died -= OnDied;
        _player.Prepeared -= OnPrepeared;
    }

    private void Update()
    {
        LookAtTarget(_targetPosition);
    }

    public void SetTarget(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    private void OnPrepeared(Transform targetPoint, Monster monster, FightType type)
    {
        if (type != FightType.Hands)
            _hands[_robotBuilder.CurrentArmIndex].SetActive(true);
    } 

    private void OnFightWon()
    {
        DisableHands();
    }

    private void OnDied(IDamageable damageable)
    {
        DisableHands();
    }

    private void DisableHands()
    {
        gameObject.SetActive(false);
    }

    private void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
}
