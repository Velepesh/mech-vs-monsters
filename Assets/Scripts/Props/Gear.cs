using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _pickUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if(player.Health.Value + _health <= player.Health.StartValue)
                player.Health.Heal(_health);
            else
                player.Health.Heal(player.Health.StartValue - player.Health.Value);

            Instantiate(_pickUpEffect.gameObject, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}