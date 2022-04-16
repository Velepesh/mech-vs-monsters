using UnityEngine;
using UnityEngine.Events;

public class House : MonoBehaviour, IDamageable, ITarget, IAward
{
    [SerializeField] private Health _health;
    [SerializeField] private int _award;
    [SerializeField] private GameObject _brokenHouse;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _offsetX = 1.2f;

    public Health Health => _health;
    public int Award => _award;
    public bool IsDied => _health.Value <= 0;
    public Vector3 Position => transform.position + new Vector3(0f, _offsetX, 0f);

    public event UnityAction<IDamageable> Died;

    private void OnValidate()
    {
        _offsetX = Mathf.Clamp(_offsetX, 0f, float.MaxValue);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);

        if (IsDied)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);

        Instantiate(_brokenHouse, transform.position, Quaternion.identity);
        _model.SetActive(false);
    }
}
