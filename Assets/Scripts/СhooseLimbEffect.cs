using UnityEngine;

[RequireComponent(typeof(LimbShop))]
public class СhooseLimbEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _chooseEffect;
    [SerializeField] private RectTransform[] _spawnPoints;

    private LimbShop _limbShop;

    private void Awake()
    {
        _limbShop = GetComponent<LimbShop>();
    }

    private void OnEnable()
    {
        _limbShop.LimbSelected += OnLimbSelected;
    }

    private void OnDisable()
    {
        _limbShop.LimbSelected -= OnLimbSelected;
    }

    private void OnLimbSelected()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
            Instantiate(_chooseEffect.gameObject, _spawnPoints[i]);
    }
}