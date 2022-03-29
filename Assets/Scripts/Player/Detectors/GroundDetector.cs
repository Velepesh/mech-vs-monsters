using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _checkDistance;

    private bool _isGround;

    public bool IsGround => _isGround;

    private void OnValidate()
    {
        _checkDistance = Mathf.Clamp(_checkDistance, 0f, float.MaxValue);
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, -transform.up, _checkDistance, _groundLayerMask))
            _isGround = true;
        else
            _isGround = false;
    }
}