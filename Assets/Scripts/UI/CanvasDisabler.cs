using UnityEngine;

public class CanvasDisabler : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _canvas;

    private void OnEnable()
    {
        _player.Prepeared += OnPrepeared;
    }

    private void OnDisable()
    {
        _player.Prepeared -= OnPrepeared;
    }

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
    {
        _canvas.SetActive(false);
    }
}