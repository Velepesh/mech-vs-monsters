using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerLimb : MonoBehaviour
{
    [SerializeField] private Limb _limb;

    public event UnityAction VisibleBecame;
    public int Health => _limb.Health;
    public Limb Limb => _limb;

    public void MakeVisible()
    {
        VisibleBecame?.Invoke();
    }
}