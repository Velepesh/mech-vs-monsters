using UnityEngine;

public class Leg : PlayerLimb
{
    [SerializeField] private MovementOptions _options;
    public float MoveSpeed => _options.MoveSpeed;
    public float RotationSpeed => _options.RotationSpeed;
    public float BorderOffset => _options.BorderOffset;
    public float StopDistance => _options.StopDistance;
    public float AttackDistance => _options.AttackDistance;
    public float LookSpeed => _options.LookSpeed;
}