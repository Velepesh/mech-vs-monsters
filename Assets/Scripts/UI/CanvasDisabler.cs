using UnityEngine;

public class CanvasDisabler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _canvas;

    private void OnEnable()
    {
        _player.Prepeared += OnPrepeared;
        _player.Fought += OnFought;
    }

    private void OnDisable()
    {
        _player.Prepeared -= OnPrepeared;
        _player.Fought -= OnFought;
    }

    private void OnPrepeared(Transform transform, Monster monster, bool isAiming)
    {
        _canvas.SetActive(false);
    }

    private void OnFought(Monster monster)
    {
        if (monster is Ahriman)
            _canvas.SetActive(true);
    }
}