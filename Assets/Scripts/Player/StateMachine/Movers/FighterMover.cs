using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(MoverOptions))]
public class FighterMover : State, IMover
{
    [SerializeField] private float _moveToTargetPointTime;

    private Player _player;
    private MoverOptions _moverOptions;
    private Vector3 _monsterPosition;
    private Vector3 _targetFightPosition;

    private void OnValidate()
    {
        _moveToTargetPointTime = Mathf.Clamp(_moveToTargetPointTime, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
        _moverOptions = GetComponent<MoverOptions>();
    }


    private void Update()
    {
        if(_monsterPosition != null)
            LookAtTarget(_monsterPosition);
    }

    public void Init(Transform targetPoint, Monster monster)
    {
        _monsterPosition = monster.Position;
        _targetFightPosition = new Vector3(targetPoint.position.x, transform.position.y, targetPoint.position.z);

        Move();
    }

    public void Move()
    {
        StartCoroutine(Move(_moveToTargetPointTime));
    }

    public void LookAtTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion lookAt = Quaternion.RotateTowards(_moverOptions.ModelRotation, targetRotation, Time.deltaTime * _moverOptions.LookSpeed);

        _moverOptions.RotateModel(lookAt);
    }

    private IEnumerator Move(float duration)
    {
        Vector3 startingPos = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float time = elapsedTime / duration;
            transform.position = Vector3.Lerp(startingPos, _targetFightPosition, time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _player.Stand();
    }
}