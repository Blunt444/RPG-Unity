using System.Collections.Generic;
using UnityEngine;

public struct EnemyStatStruct
{
    public float speed;
    public float attackCooldown;
    public float playerDetectionRange;
    public float attackRange;
    public int maxHealth;
    public int expReward;
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockBackTime;
    public int guardDamage;

}

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
