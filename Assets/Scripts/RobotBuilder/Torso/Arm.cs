using UnityEngine;
using System.Collections.Generic;

public class Arm : PlayerLimb
{
    [SerializeField] private List<GameObject> _arms;
    [SerializeField] private List<Machinegun> _machineguns;
    [SerializeField] private List<Machinegun> _currentMachineguns;

    public void EnableArms()
    {
        for (int i = 0; i < _arms.Count; i++)
            _arms[i].SetActive(true);

        DisableOtherGuns();
        EnableGuns();
    }

    public void DisableArms()
    {
        for (int i = 0; i < _arms.Count; i++)
            _arms[i].SetActive(false);
    }

    private void EnableGuns()
    {
        for (int i = 0; i < _currentMachineguns.Count; i++)
            _currentMachineguns[i].gameObject.SetActive(true);
    }

    private void DisableOtherGuns()
    {
        for (int i = 0; i < _machineguns.Count; i++)
            _machineguns[i].gameObject.SetActive(false);
    }
}