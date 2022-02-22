using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField] private ParticleSystem _pickUpEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out GearsCollector gearsCollector))
        {
            gearsCollector.AddGear();
            gameObject.SetActive(false);

            Instantiate(_pickUpEffect.gameObject, transform.position, Quaternion.identity);
        }
    }
}