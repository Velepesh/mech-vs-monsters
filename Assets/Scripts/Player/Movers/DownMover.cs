using UnityEngine;
using UnityEngine.Events;

public class DownMover : State, IMover
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private float _speed;

    public event UnityAction Landed;

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0f, float.MaxValue);
    }

    private void FixedUpdate()
    {
        Move();

        if (_groundDetector.IsGround)
            Landed?.Invoke();
    }

    public void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed, Space.World);
    }

    public void LookAtTarget(Vector3 target)
    {
        throw new System.NotImplementedException();
    }
}