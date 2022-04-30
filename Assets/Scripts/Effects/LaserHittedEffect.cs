using UnityEngine;

public class LaserHittedEffect : MonoBehaviour
{
    [SerializeField] private LaserGun _laserGun;
    [SerializeField] private ParticleSystem _hitEffect;

    private void OnEnable()
    {
        _laserGun.Hitted += OnHitted;
    }

    private void OnDisable()
    {
        _laserGun.Hitted -= OnHitted;
    }

    private void OnHitted(Vector3 hitPoint)
    {
        Instantiate(_hitEffect.gameObject, hitPoint, Quaternion.identity);
    }
}