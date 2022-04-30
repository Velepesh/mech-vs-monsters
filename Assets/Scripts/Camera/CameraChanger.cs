using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] private Camera _thirdPersonCamera;
    [SerializeField] private Camera _povCamera;
    [SerializeField] private Player _player;

    private Camera _currentCamera;

    public Camera Camera => _currentCamera;
    private void OnEnable()
    {
        _player.Prepeared += OnPrepeared;
        _player.FightWon += OnFightWon;
    }

    private void OnDisable()
    {
        _player.Prepeared -= OnPrepeared;
        _player.FightWon -= OnFightWon;
    }

    private void Start()
    {
        ChangeOnThirdPersonCamera();
    }

    private void OnPrepeared(Transform transform, Monster monster, FightType type)
    {
        if(type != FightType.Hands)
            ChangeOnPovCamera();
    }

    private void OnFightWon()
    {
        ChangeOnThirdPersonCamera();
    }

    private void ChangeOnPovCamera()
    {
        _currentCamera = _povCamera;
        _povCamera.gameObject.SetActive(true);
    }

    private void ChangeOnThirdPersonCamera()
    {
        _currentCamera = _thirdPersonCamera;
        _povCamera.gameObject.SetActive(false);
    }
}
