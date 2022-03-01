using UnityEngine;
using System.Collections.Generic;

public class Arm : PlayerLimb
{
    [SerializeField] private List<GameObject> _arms;

    public void EnableArms()
    {
        for (int i = 0; i < _arms.Count; i++)
            _arms[i].SetActive(true);
    }

    public void DisableArms()
    {
        for (int i = 0; i < _arms.Count; i++)
            _arms[i].SetActive(false);
    }
}