using UnityEngine;

public interface IMover 
{
    void Move();
    void LookAtTarget(Vector3 target);
}