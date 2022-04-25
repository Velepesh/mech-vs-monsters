using UnityEngine;
using System.Collections.Generic;

public class Body : PlayerLimb
{
    [SerializeField] private List<Machinegun> _machineguns;
    [SerializeField] private List<Machinegun> _currentMachineguns;

    public void EnableGuns()
    {
        DisableOtherGuns();

        for (int i = 0; i < _currentMachineguns.Count; i++)
            _currentMachineguns[i].gameObject.SetActive(true);
    }

    private void DisableOtherGuns()
    {
        for (int i = 0; i < _machineguns.Count; i++)
            _machineguns[i].gameObject.SetActive(false);
    }
}