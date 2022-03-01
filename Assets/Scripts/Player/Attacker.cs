using UnityEngine;
using UnityEngine.Events;

public class Attacker : MonoBehaviour
{
    [SerializeField] private float _cooldownTime;

    private float _shootingTimer;

    public event UnityAction Attacked;

    private void Update()
    {
        _shootingTimer += Time.deltaTime;
    }

    public void Attack()
    {
        if (_shootingTimer >= _cooldownTime)
        {
            Attacked?.Invoke();

            _shootingTimer = 0;
        }
    }

    public void Reload()
    {
        _shootingTimer = _cooldownTime;
    }
}