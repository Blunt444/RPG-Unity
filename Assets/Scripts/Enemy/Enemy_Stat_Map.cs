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
    public float attackCooldownBuffer;
    public float attackRange;
    public float weaponRange;
    public int damage;
    public int guardDamage;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockBackTime;
    public float knockBackTimeResistance;
    public float stuntResistance;

    [Header("Loot")]
    public int minDrop;
    public int maxDrop;
    public int minQuantity;
    public int maxQuantity;
    public List<ItemSO> loots;

}

[Serializable]
public struct EnemyStatEntry
{
    public Enemy_Type type;
    public Enemy_Difficulty difficulty;
    public EnemyStatStruct stat;
}

public class Enemy_Stat_Map : MonoBehaviour
{
    public static Enemy_Stat_Map Instance;

    [SerializeField]
    private List<EnemyStatEntry> entryList = new List<EnemyStatEntry>();

    private Dictionary<(Enemy_Type, Enemy_Difficulty), EnemyStatStruct> statDict = new Dictionary<(Enemy_Type, Enemy_Difficulty), EnemyStatStruct>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            foreach (EnemyStatEntry entry in entryList)
            {
                statDict[(entry.type, entry.difficulty)] = entry.stat;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public EnemyStatStruct GetEnemyStat(Enemy_Type enemyType, Enemy_Difficulty difficulty)
    {
        return statDict[(enemyType, difficulty)];
    }

    public void UpdateEnemyStat(Enemy_Type enemyType)
    {

    }
}
