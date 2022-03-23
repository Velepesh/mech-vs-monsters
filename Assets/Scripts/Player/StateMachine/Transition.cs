using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Enemy Target { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    public void Init(Enemy target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}