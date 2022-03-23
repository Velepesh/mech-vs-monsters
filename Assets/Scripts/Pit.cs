using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Pit : MonoBehaviour
{
    [SerializeField] private ParticleSystem _waterSplashEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.Fall();
            var effect = Instantiate(_waterSplashEffect.gameObject, player.transform.position, Quaternion.identity);
        }
    }
}