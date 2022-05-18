using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour, IShooteable
{
    private ITarget _target;
    private ITarget _thisITarget;
    private Vector3 _targetPosition;
    private bool _isShooting;
    private bool _isAiming;

    public ITarget Target => _target;
    public ITarget ThisITarget => _thisITarget;
    public Vector3 TargetPosition => _targetPosition;
    public bool IsShooting => _isShooting;
    public bool IsAiming => _isAiming;

    public abstract event UnityAction Shooted;
    public event UnityAction LostTarget;

    public void SetTarget(ITarget target, ITarget thisITarget)
    {
        _target = target;
        _thisITarget = thisITarget;
    }

    public void SetTarget(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _isAiming = true;
        StartShooting();
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public void TurnToTarget(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    public void StartShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;

        LostTarget?.Invoke();
    }
}