using UnityEngine;

public class AttackHelicopter : Vehicle, IDamageable, IMover, ITarget
{
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _sidewaySpeed;
    [SerializeField] private float _moveOffsetX;

    private float _startPositionX;
    private Vector3 _targetPosition;

    private void OnValidate()
    {
        _distanceToPlayer = Mathf.Clamp(_distanceToPlayer, 0f, float.MaxValue);
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
    }
    private void Update()
    {
        if (Target != null && Target.IsDied == false)
        {
            Move();
            LookAtTarget(Target.Position);
        }
    }
    public void Move()
    {
        _targetPosition = new Vector3(transform.position.x, transform.position.y, Target.Position.z + _distanceToPlayer);
        
        if (transform.position.z < _targetPosition.z)
            MoveSideway();
        else
            StopSideway();

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);

        transform.rotation = lookAt;
    }
    private void MoveSideway()
    {
        _targetPosition = new Vector3(Mathf.Lerp(_startPositionX, _startPositionX + _moveOffsetX, (Mathf.Sin(_sidewaySpeed * Time.time) + 1.0f) / 2.0f), _targetPosition.y, _targetPosition.z);
    }

    private void StopSideway()
    {
        _startPositionX = transform.position.x;
    }
}