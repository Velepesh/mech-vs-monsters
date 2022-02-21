using UnityEngine;

public class Head : PlayerLimb
{
    [SerializeField] private Collider _headCollider;

    public void EnableCollider()
    {
        _headCollider.gameObject.SetActive(true);
    }

    public void Disableollider()
    {
        _headCollider.gameObject.SetActive(false);
    }
}