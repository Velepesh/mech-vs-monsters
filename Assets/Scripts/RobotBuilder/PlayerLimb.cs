using UnityEngine;

public abstract class PlayerLimb : MonoBehaviour
{
    [SerializeField] private Limb _limb;

    public int Health => _limb.Health;
    public Limb Limb => _limb;
}