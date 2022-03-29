using UnityEngine;

public class PlayerDestroyer : State
{
    [SerializeField] private GameObject _model;

    private void Start()
    {
        _model.SetActive(false);
    }
}