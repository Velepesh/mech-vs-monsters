using UnityEngine;

[RequireComponent(typeof(Player))]
public class MoverOptions : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private StopDetector _stopDetector;

    private Player _player;
    private Leg _currentLeg;
    private bool _isNearEnemy;
    private bool _isNearObstacle;

    public Leg CurrentLeg => _currentLeg;
    public float MoveSpeed => _currentLeg.MoveSpeed;
    public float StopDistance => _currentLeg.StopDistance;
    public float AttackDistance => _currentLeg.AttackDistance;
    public float BorderOffset => _currentLeg.BorderOffset;
    public float RotationSpeed => _currentLeg.RotationSpeed;
    public float LookSpeed => _currentLeg.LookSpeed;
    public Quaternion ModelRotation => _model.transform.rotation;
    public bool IsNearEnemy => _isNearEnemy;
    public bool IsNearObstacle => _isNearObstacle;

    public void RotateModel(Quaternion lookAtTarget)
    {
        _model.transform.rotation = lookAtTarget;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.LegChanged += OnLegChanged;
    }

    private void OnDisable()
    {
        _player.LegChanged -= OnLegChanged;
    }

    private void OnLegChanged(Leg leg)
    {
        _currentLeg = leg;
    }

    private void Update()
    {
        if (_currentLeg != null)
        {
            _isNearEnemy = _stopDetector.IsNearEnemy(StopDistance);
            _isNearObstacle = _stopDetector.IsNearObstacle(StopDistance);
        }
    }
}