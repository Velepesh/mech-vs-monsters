using UnityEngine;

public class CameraConstPosition : MonoBehaviour
{
    private float _startPositionX;

    private void Start()
    {
        _startPositionX = transform.position.x;
    }

    private void Update()
    {
        transform.position = new Vector3(_startPositionX, transform.position.y, transform.position.z);
    }
}