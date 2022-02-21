using UnityEngine;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviour, IShooteable
{
    private ITarget _target;
    private bool _isShooting;

    public ITarget Target => _target;
    public bool IsShooting => _isShooting;

    public abstract event UnityAction Shooted;
    public event UnityAction LostTarget;

    public void SetTarget(ITarget target, ITarget thisITarget)
    {
        _target = target;
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