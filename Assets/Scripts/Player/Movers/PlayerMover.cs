using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MoverOptions))]
public class PlayerMover : State, IMover
{
    readonly private float _rotationAngle = 45f;
    readonly private float _tutorialMoveSpeed = 0f;

    private MoverOptions _moverOptions;
    private Player _player;
    private PlayerInput _input;
    private bool _isTutorial;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = GetComponent<PlayerInput>();
        _moverOptions = GetComponent<MoverOptions>();
    }

    private void Update()
    {
        if (_isTutorial)
            MoveInSpace();
        else
            Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.forward * _moverOptions.MoveSpeed * Time.deltaTime);
        
        if (_player.IsAiming)
            Rotate(0f);
        else
            Swipe();
    }

    public void StartTutorialMove()
    {
        _isTutorial = true;
    }

    public void EndTutorialMove()
    {
        _isTutorial = false;
    }

    public void LookAtTarget(Vector3 target)
    {
        throw new System.NotImplementedException();
    }

    private void MoveInSpace()
    {
        transform.Translate(Vector3.forward * _tutorialMoveSpeed * Time.deltaTime);
    }

    private void Swipe()
    {
        float swerveAmount = Time.deltaTime * _input.Sensitivity * Mathf.Clamp(_input.MoveFactorX, -1f, 1);
        swerveAmount = Mathf.Clamp(swerveAmount, -_moverOptions.BorderOffset, _moverOptions.BorderOffset);
         
        if (swerveAmount > 0f)
            TryMoveRight(swerveAmount);
        else if (swerveAmount < 0f)
            TryMoveLeft(swerveAmount);
        else
            Rotate(0f);
    }

    private void TryMoveRight(float swerveAmount)
    {
        Vector3 position = transform.position;

        if (position.x >= _moverOptions.BorderOffset)
            ReachBorder(position, _moverOptions.BorderOffset);
        else
            MoveSideways(swerveAmount);
    }

    private void TryMoveLeft(float swerveAmount)
    {
        Vector3 position = transform.position;

        if (position.x <= -_moverOptions.BorderOffset)
            ReachBorder(position, -_moverOptions.BorderOffset);
        else
            MoveSideways(swerveAmount);
    }

    private void ReachBorder(Vector3 position, float border)
    {
        transform.position = new Vector3(border, position.y, position.z);
        Rotate(0f);
    }

    private void MoveSideways(float swerveAmount)
    {
        transform.Translate(swerveAmount, 0, 0);
        Rotate(Mathf.Clamp(_input.MoveFactorX, -1f, 1));
    }

    private void Rotate(float directionY)
    {
        var currentRotation = _moverOptions.ModelRotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, directionY * _rotationAngle, 0));

        _moverOptions.RotateModel(Quaternion.Lerp(currentRotation, targetRotation, _moverOptions.RotationSpeed * Time.deltaTime));
    }
}