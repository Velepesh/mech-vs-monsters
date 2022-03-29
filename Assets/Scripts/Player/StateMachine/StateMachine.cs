using UnityEngine;

[RequireComponent(typeof(Player))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Enemy _target;
    private Player _player;
    private State _currentState;

    public State Current => _currentState;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _player.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }
}