using UnityEngine;

public class AnimatorEnemyController : MonoBehaviour
{
    public static class States
    {
        public const string Shoot = nameof(Shoot);
        public const string Idle = nameof(Idle);
        public const string IsMove = nameof(IsMove);
        public const string IsMoveBack = nameof(IsMoveBack);
    }
}