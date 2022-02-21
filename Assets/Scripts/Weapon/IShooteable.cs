using UnityEngine;
public interface IShooteable
{
    void Shoot();
    void SetTarget(ITarget damageable, ITarget thisITarget);
    void TurnToTarget(Vector3 target);
}