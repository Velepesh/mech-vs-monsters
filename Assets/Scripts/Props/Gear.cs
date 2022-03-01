using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _pickUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out GearsCollector gearsCollector))
        {
            gearsCollector.AddGear();
            gameObject.SetActive(false);

            Instantiate(_pickUpEffect.gameObject, transform.position, Quaternion.identity);
        }

        if (other.TryGetComponent(out Player player))
        {
            if(player.Health + _health <= player.StartHealth)
                player.AddHealth(_health);
            else
                player.AddHealth(player.StartHealth - player.Health);
        }
    }
}