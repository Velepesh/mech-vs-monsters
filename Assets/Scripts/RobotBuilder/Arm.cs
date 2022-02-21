using UnityEngine;
using System.Collections.Generic;

public class Arm : PlayerLimb
{
    [SerializeField] private List<Collider> _armColliders;

    public void EnableCollider()
    {
        for (int i = 0; i < _armColliders.Count; i++)
            _armColliders[i].gameObject.SetActive(true);
    }

    public void Disableollider()
    {
        for (int i = 0; i < _armColliders.Count; i++)
            _armColliders[i].gameObject.SetActive(false);
    }
}