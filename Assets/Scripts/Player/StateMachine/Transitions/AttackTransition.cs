using UnityEngine;

[RequireComponent(typeof(MoverOptions))]
public class AttackTransition : Transition
{
    private MoverOptions _moverOptions;

    private void Awake()
    {
        NeedTransit = false;
        _moverOptions = GetComponent<MoverOptions>();
    }

    private void Update()
    {
        if (_moverOptions.IsNearEnemy || _moverOptions.IsNearObstacle)
            NeedTransit = true;
    }
}