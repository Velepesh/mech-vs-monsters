using UnityEngine;
using System.Collections.Generic;

public class Head : PlayerLimb
{
    [SerializeField] private GameObject _head;
    [SerializeField] private List<Machinegun> _machineguns;
    [SerializeField] private Machinegun _currentMachineguns;

    public void EnableHead()
    {
        _head.SetActive(true);

        DisableOtherGuns();
        EnebleGun();
    }

    public void DisableHead()
    {
        _head.SetActive(false);
    }

    private void EnebleGun()
    {
        _currentMachineguns.gameObject.SetActive(true);
    }

    private void DisableOtherGuns()
    {
        for (int i = 0; i < _machineguns.Count; i++)
            _machineguns[i].gameObject.SetActive(false);
    }
}