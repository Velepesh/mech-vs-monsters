using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MoverOptions))]
public class PlayerMover : State, IMover
{
    readonly private float _rotationAngle = 45f;
    readonly private float _tutorialMoveSpeed = 0f;
    readonly private float _blindAreaX = 8f;


    private MoverOptions _moverOptions;
    private PlayerInput _input;
    private bool _isTutorial;
    private float _positionX;
    private float _previousPositionX;

    private void Awake()
    {
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

        if (_input.IsHold)
        {
            Swipe(_input.SwipePosition);
        }
        else
        {
            Rotate(0f);
            _previousPositionX = 0;
        }
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

    private void Swipe(Vector3 position)
    {
        _positionX = position.x;
      
        ApplySwipePosition();
        
        float swerveAmount = _input.Sensitivity * Mathf.Clamp(_positionX, -1f, 1);

        if (swerveAmount > 0f)
            TryMoveRight(swerveAmount, _positionX);
        else if (swerveAmount < 0f)
            TryMoveLeft(swerveAmount, _positionX);
        else
            Rotate(0f);

    }

    private void ApplySwipePosition()
    {
        if (_previousPositionX < 0f && _positionX <= 0f)
            _positionX = -1f;
        else if (_previousPositionX > 0f && _positionX >= 0f)
            _positionX = 1f;
        else if (_positionX == 0 || Mathf.Abs(_positionX) < _blindAreaX)
            _positionX = 0;

        _previousPositionX = _positionX;
    }

    private void ReachBorder(Vector3 position, float border)
    {
        transform.position = new Vector3(border, position.y, position.z);
        Rotate(0f);
    }

    private void Rotate(float directionY)
    {
        var currentRotation = _moverOptions.ModelRotation;
        var targetRotation = Quaternion.Euler(new Vector3(0, directionY * _rotationAngle, 0));

        _moverOptions.RotateModel(Quaternion.Lerp(currentRotation, targetRotation, _moverOptions.RotationSpeed * Time.deltaTime));
    }

    private void TryMoveRight(float swerveAmount, float positionX)
    {
        Vector3 position = transform.position;

        if (position.x >= _moverOptions.BorderOffset)
            ReachBorder(position, _moverOptions.BorderOffset);
        else
            MoveSideways(swerveAmount, positionX);
    }

    private void TryMoveLeft(float swerveAmount, float positionX)
    {
        Vector3 position = transform.position;

        if (position.x <= -_moverOptions.BorderOffset)
            ReachBorder(position, -_moverOptions.BorderOffset);
        else
            MoveSideways(swerveAmount, positionX);
    }

    private void MoveSideways(float swerveAmount, float positionX)
    {
        transform.Translate(new Vector3(swerveAmount, 0, 0) * Time.deltaTime);
        Rotate(Mathf.Clamp(positionX, -1f, 1));
    }
}