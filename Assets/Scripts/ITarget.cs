using UnityEngine;

public interface ITarget
{
    bool IsDied { get; }
    Vector3 GetPosition();
}