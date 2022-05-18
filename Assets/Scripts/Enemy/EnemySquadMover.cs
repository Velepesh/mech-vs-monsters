using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemySquad))]
public class EnemySquadMover : MonoBehaviour
{
    private EnemySquad _enemySquad;

    private void Awake()
    {
        _enemySquad = GetComponent<EnemySquad>();
    }
}
