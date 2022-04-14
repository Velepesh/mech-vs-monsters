using UnityEngine;

public class AttackHelicopter : Vehicle, IDamageable, IMover, ITarget
{
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _sidewaySpeed;
    [SerializeField] private float _moveOffsetX;

    private float _startPosition;
    private Vector3 _targetPosition;

    private void OnValidate()
    {
        _distanceToPlayer = Mathf.Clamp(_distanceToPlayer, 0f, float.MaxValue);
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _sidewaySpeed = Mathf.Clamp(_sidewaySpeed, 0f, float.MaxValue);
        _moveOffsetX = Mathf.Clamp(_moveOffsetX, 0f, float.MaxValue);
    }

    private void Start()
    {
        _startPosition = transform.position.x;
    }

    private void Update()
    {
        if (Target != null && Target.IsDied == false)
            Move();
        else
            MoveInSpace();
    }
    public void Move()
    {
        LookAtTarget(Target.Position);

        float leftX = _startPosition - _moveOffsetX + Target.Position.x;
        float rightX = _startPosition + _moveOffsetX + Target.Position.x;

        _targetPosition = new Vector3(GetSidewayX(leftX, rightX), transform.position.y, Target.Position.z + _distanceToPlayer);

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);

        transform.rotation = lookAt;
    }

    private void MoveInSpace()
    {
        float leftX = _startPosition - _moveOffsetX;
        float rightX = _startPosition + _moveOffsetX;

        _targetPosition = new Vector3(GetSidewayX(leftX, rightX), transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
    private float GetSidewayX(float leftX, float rightX)
    {
        return Mathf.Lerp(leftX, rightX, (Mathf.Sin(_sidewaySpeed * Time.time) + 1.0f) / 2.0f);
    }
}