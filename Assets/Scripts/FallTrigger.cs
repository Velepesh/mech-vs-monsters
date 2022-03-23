using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            player.Die();
    }
}
