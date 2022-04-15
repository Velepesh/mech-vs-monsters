using UnityEngine;

public class DamagedEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _damagedEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out DamageCollider damageCollider))
            Instantiate(_damagedEffect.gameObject, collision.contacts[0].point, Quaternion.identity);
    }
}