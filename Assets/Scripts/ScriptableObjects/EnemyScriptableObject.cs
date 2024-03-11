using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public enum EnemyType
    {
        SPRINT,
        HEAVY,
        FLYING,
        EXPOLODER
    }
    public string EnemyName;
    public int health, attack, speed, moneyReward;
    public bool canCollideWithTowers;
    public EnemyType enemyType;
    public GameObject deathParticles;
    public GameObject healParticels;
    public GameObject poisonParticles;
}
