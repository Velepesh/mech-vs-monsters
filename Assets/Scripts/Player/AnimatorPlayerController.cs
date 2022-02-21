using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerController : MonoBehaviour
{
    public static class States
    {
        public const string Move = nameof(Move);
        public const string ShotRocket = nameof(ShotRocket);
        public const string Stop = nameof(Stop);
        public const string ArmAttack = nameof(ArmAttack);
        public const string LegAttack = nameof(LegAttack);
        public const string Stand = nameof(Stand);
        public const string Win = nameof(Win);
    }
}
