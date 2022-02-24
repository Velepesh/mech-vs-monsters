using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class DownMover : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance;

    private Player _player;
    private bool _canMove;

    public event UnityAction Landed;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
        _checkDistance = Mathf.Clamp(_checkDistance, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _player.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed, Space.World);

            if (Physics.Raycast(transform.position, -transform.up, _checkDistance, _groundLayerMask))
            {
                _canMove = false;
                Landed?.Invoke();
            }
        }
           
    }
}