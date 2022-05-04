using UnityEngine;
using System.Collections.Generic;

public class Ground : MonoBehaviour
{
    [SerializeField] private GroundType _type;
    [SerializeField] private List<GameObject> _props;

    private bool _isPlayerStaying = false;
    public GroundType Type => _type;

    public bool IsPlayerStaying => _isPlayerStaying;

    public void DisableProps()
    {
        for (int i = 0; i < _props.Count; i++)
            _props[i].SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            _isPlayerStaying = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
            _isPlayerStaying = false;
    }
}