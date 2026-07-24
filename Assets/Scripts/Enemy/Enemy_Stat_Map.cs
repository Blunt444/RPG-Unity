using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyStatStruct
{
    [Header("Health & Rewards")]
    public int maxHealth;
    public int expReward;

    [Header("Movement & Detection")]
    public float speed;
    public float playerDetectionRange;

    [Header("Combat Stats")]
    public float attackCooldown;
    public float attackRange;
    public float weaponRange;
    public int damage;
    public int guardDamage;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockBackTime;

}

[Serializable]
public struct EnemyStatEntry
{
    public Enemy_Type key;
    public EnemyStatStruct stat;
}

public class Enemy_Stat_Map : MonoBehaviour
{
    public static Enemy_Stat_Map Instance;

    [SerializeField]
    private List<EnemyStatEntry> entryList = new List<EnemyStatEntry>();

    private Dictionary<Enemy_Type, EnemyStatStruct> statDict = new Dictionary<Enemy_Type, EnemyStatStruct>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            foreach (EnemyStatEntry entry in entryList)
            {
                statDict[entry.key] = entry.stat;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public EnemyStatStruct GetEnemyStat(Enemy_Type enemyType)
    {
        return statDict[enemyType];
    }

    public void UpdateEnemyStat(Enemy_Type enemyType)
    {

    }
}
