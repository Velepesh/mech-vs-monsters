using UnityEngine;

public class StopDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private BoxCollider _searchingEnemycollider;
    [SerializeField] private BoxCollider _searchingObstaclecollider;

    public bool IsNearEnemy(float distance)
    {
        return IsHeat(_searchingEnemycollider, _enemyMask, distance);
    }

    public bool IsNearObstacle(float distance)
    {
        return IsHeat(_searchingObstaclecollider, _obstacleMask, distance);
    }

    private bool IsHeat(BoxCollider collider, LayerMask mask, float distance)
    {
        return Physics.BoxCast(collider.bounds.center, collider.transform.localScale / 2f, transform.forward, transform.rotation, distance, mask);
    }
}