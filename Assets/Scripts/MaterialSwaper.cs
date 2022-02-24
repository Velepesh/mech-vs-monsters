using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialSwaper : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Material _slowWaterMat;
    [SerializeField] private Material _fastWaterMat;
    [SerializeField] private float _delay;

    private MeshRenderer _mesh;

    private void OnValidate()
    {
        _delay = Mathf.Clamp(_delay, 0f, float.MaxValue);
    }

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
        _mesh.material = _fastWaterMat;
    }

    private void OnEnable()
    {
        _game.LevelStarted += OnLevelStarted;
    }

    private void OnDisable()
    {
        _game.LevelStarted -= OnLevelStarted;
    }

    private void OnLevelStarted()
    {
        StartCoroutine(ChangeMaterial());
    }

    private IEnumerator ChangeMaterial()
    {
        yield return new WaitForSeconds(_delay);
        _mesh.material = _slowWaterMat;
    }
}