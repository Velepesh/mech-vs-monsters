using System.Collections.Generic;
using UnityEngine;

public class EnemiesHolder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _maxRange = 10;
    
    private List<Enemy> _enemies = new List<Enemy>();

    private void OnValidate()
    {
        _maxRange = Mathf.Clamp(_maxRange, 0f, float.MaxValue);
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out IDamageable damageable))
            {
                if(damageable is Enemy enemy)
                {
                    _enemies.Add(enemy);
                    damageable.Died += OnDied;
                }
                
            }
        }
    }

    private void OnDied(IDamageable damageable)
    {
        if (damageable is Enemy enemy)
        {
            _player.AddMoney(enemy.Award);
            damageable.Died -= OnDied;
        }
    }
}