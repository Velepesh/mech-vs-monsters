using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorGunsIcon : MonoBehaviour
{
    [SerializeField] private GameObject[] _guns;

    public void ActivateGun()
    {
        StartCoroutine(StartActivate());
    }

    private IEnumerator StartActivate()
    {
        yield return new WaitForSeconds(0.3f);

        foreach (var gun in _guns)
        {
            gun.gameObject.SetActive(true);
        }
    }
}
