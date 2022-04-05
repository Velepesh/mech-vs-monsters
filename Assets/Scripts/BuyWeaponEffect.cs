using UnityEngine;

[RequireComponent(typeof(WeaponShop))]
public class BuyWeaponEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _buyEffect;

    private WeaponShop _weaponShop;

    private void Awake()
    {
        _weaponShop = GetComponent<WeaponShop>();
    }

    private void OnEnable()
    {
        _weaponShop.Buied += OnBuied;
    }

    private void OnDisable()
    {
        _weaponShop.Buied -= OnBuied;
    }

    private void OnBuied()
    {
        Instantiate(_buyEffect.gameObject, transform);
    }
}