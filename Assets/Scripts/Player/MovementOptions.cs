using System;
using UnityEngine;

[Serializable]
public class MovementOptions
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _borderOffset;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _attackDistance;

    public float MoveSpeed => _moveSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float BorderOffset => _borderOffset;
    public float StopDistance => _stopDistance;
    public float AttackDistance => _attackDistance;
}