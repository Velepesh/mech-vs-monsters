using UnityEngine;

public class Head : PlayerLimb
{
    [SerializeField] private GameObject _head;

    public void EnableHead()
    {
        _head.SetActive(true);
    }

    public void DisableHead()
    {
        _head.SetActive(false);
    }
}