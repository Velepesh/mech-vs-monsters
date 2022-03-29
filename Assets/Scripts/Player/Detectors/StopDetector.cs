using UnityEngine;

public class StopDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private BoxCollider _searchingEnemycollider;
    [SerializeField] private BoxCollider _searchingObstaclecollider;

    public bool IsNearEnemy(float distance)
    {
        bool isHit = IsHeat(_searchingEnemycollider, _enemyMask, distance);

        if (isHit)
            return true;
        else
            return false;
    }

    public bool IsNearObstacle(float distance)
    {
        bool isHit = IsHeat(_searchingObstaclecollider, _obstacleMask, distance);

        if (isHit)
            return true;
        else
            return false;
    }

    private bool IsHeat(BoxCollider collider, LayerMask mask, float distance)
    {
        bool isHit = Physics.BoxCast(collider.bounds.center, collider.transform.localScale / 2f, transform.forward, transform.rotation, distance, mask);

        if (isHit)
            return true;
        else
            return false;
    }
}