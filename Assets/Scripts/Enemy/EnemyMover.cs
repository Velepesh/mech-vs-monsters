using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour, IMover
{
    [SerializeField] protected float DistanceToPlayer;
    [SerializeField] protected float Speed;
    [SerializeField] protected float LookSpeed;
    [SerializeField] protected float OffsetToTargetPositionZ = 20f;

    private Enemy _enemy;
    private float _startSpeed;

    protected Vector3 TargetPosition;
    protected Vector3 Offset;

    public event UnityAction Moved;
    public event UnityAction Stopped;


    private void OnValidate()
    {
        DistanceToPlayer = Mathf.Clamp(DistanceToPlayer, 0f, float.MaxValue);
        Speed = Mathf.Clamp(Speed, 0f, float.MaxValue);
        LookSpeed = Mathf.Clamp(LookSpeed, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _startSpeed = Speed;
    }

    private void OnEnable()
    {
        _enemy.Moved += OnMoved;
        _enemy.Stopped += OnStopped;
    }

    private void OnDisable()
    {
        _enemy.Moved -= OnMoved;
        _enemy.Stopped -= OnStopped;
    }

    private void Start()
    {
        Offset.x = _enemy.Target.Position.x - transform.position.x;

        Moved?.Invoke();
    }

    private void Update()
    {
        if (Speed > 0)
        {
            if (_enemy.Target != null && _enemy.Target.IsDied == false)
                Move();
            else
                MoveWithoutTarget();
        }
    }

    public void Move()
    {
        LookAtTarget(_enemy.Target.Position);

        TargetPosition = new Vector3(_enemy.Target.Position.x - Offset.x, transform.position.y, _enemy.Target.Position.z + DistanceToPlayer);

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);

        if (Mathf.Abs(TargetPosition.z - transform.position.z) <= OffsetToTargetPositionZ)
            _enemy.InitTargetForWeapon();
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * LookSpeed);

        transform.rotation = lookAt;
    }

    protected void MoveWithoutTarget()
    {
        transform.Translate(-Vector3.forward * Speed * Time.deltaTime, Space.World);
    }

    private void OnStopped()
    {
        Speed = 0;
    }

    private void OnMoved()
    {
        Speed = _startSpeed;
    }
}