using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StopDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private BoxCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }
   
    public bool IsHeat(float distance)
    {
        bool isHit = Physics.BoxCast(_collider.bounds.center, transform.localScale / 2f, transform.forward, transform.rotation, distance, _layerMask);

        if (isHit)
            return true;
        else
            return false;
    }
}