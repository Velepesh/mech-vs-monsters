using UnityEngine;

public class AnimatorGodzillaController : MonoBehaviour
{
    public static class States
    {
        public const string Win = nameof(Win);
        public const string Death = nameof(Death);
        public const string Attack = nameof(Attack);
        public const string IsAttack = nameof(IsAttack);
    }
}