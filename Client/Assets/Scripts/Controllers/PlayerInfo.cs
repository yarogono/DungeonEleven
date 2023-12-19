using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    [Range(1f, 20f)] public float jumpingPower;

    // 공격 데이터
    public AttackSO attackSO;

    public int attack;
    public int def;
    public float evasion;
    public int gold;
    public List<Item> inventory = new List<Item>();
    public float level = 1;
}