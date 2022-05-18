using UnityEngine;

[RequireComponent(typeof(AttackHelicopter))]
public class AttackHelicopterMover : EnemyMover
{
    [SerializeField] private float _sidewaySpeed;
    [SerializeField] private float _moveOffsetX;

    private AttackHelicopter _attackHelicopter;
    private float _startPositionX;

    private void OnValidate()
    {
        _sidewaySpeed = Mathf.Clamp(_sidewaySpeed, 0f, float.MaxValue);
        _moveOffsetX = Mathf.Clamp(_moveOffsetX, 0f, float.MaxValue);
    }

    private void Start()
    {
        _attackHelicopter = GetComponent<AttackHelicopter>();
        _startPositionX = transform.position.x;
        Offset.x = _attackHelicopter.Target.Position.x - transform.position.x;
    }

    private void Update()
    {
        if (_attackHelicopter.Target != null && _attackHelicopter.Target.IsDied == false)
            Move();
        else
            MoveWithoutTarget();
    }

    public new void Move()
    {
        LookAtTarget(_attackHelicopter.Target.Position);

        if (Mathf.Abs(TargetPosition.z - transform.position.z) <= OffsetToTargetPositionZ)
            _attackHelicopter.InitTargetForWeapon();
     
        if(transform.position.z - _attackHelicopter.Target.Position.z > DistanceToPlayer + OffsetToTargetPositionZ)
        {
            TargetPosition = new Vector3(_attackHelicopter.Target.Position.x - Offset.x, transform.position.y, _attackHelicopter.Target.Position.z + DistanceToPlayer);
        }
        else
        {
            float leftX = -_moveOffsetX + _attackHelicopter.Target.Position.x - Offset.x;
            float rightX = _moveOffsetX + _attackHelicopter.Target.Position.x - Offset.x;

            TargetPosition = new Vector3(GetSidewayX(leftX, rightX), transform.position.y, _attackHelicopter.Target.Position.z + DistanceToPlayer);
        }

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
    }

    public new void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);

        transform.rotation = lookAt;
    }

    private float GetSidewayX(float leftX, float rightX)
    {
        return Mathf.Lerp(leftX, rightX, (Mathf.Sin(_sidewaySpeed * Time.time) + 1.0f) / 2.0f);
    }
}