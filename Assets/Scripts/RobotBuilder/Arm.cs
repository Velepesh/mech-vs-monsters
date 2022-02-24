using UnityEngine;
using System.Collections.Generic;

public class Arm : PlayerLimb
{
    [SerializeField] private List<GameObject> _arms;

    public void EnableCollider()
    {
        for (int i = 0; i < _arms.Count; i++)
            _arms[i].SetActive(true);
    }

    public void Disableollider()
    {
        for (int i = 0; i < _arms.Count; i++)
            _arms[i].SetActive(false);
    }
}