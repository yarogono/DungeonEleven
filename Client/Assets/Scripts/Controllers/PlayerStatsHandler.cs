using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{

    [SerializeField] private PlayerInfo baseStats;
    public PlayerInfo CurrentStates { get; private set; }
    public List<PlayerInfo> statsModifiers = new List<PlayerInfo>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }

        CurrentStates = new PlayerInfo { attackSO = attackSO };
        // TODO
        CurrentStates.maxHealth = baseStats.maxHealth;
        CurrentStates.speed = baseStats.speed;

    }
}
