using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    [Range(1f, 20f)] public float jumpingPower;

    // ���� ������
    public AttackSO attackSO;
}