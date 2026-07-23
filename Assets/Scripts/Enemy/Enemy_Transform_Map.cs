using System.Collections.Generic;
using UnityEngine;

public struct EnemyTransform
{
    public Transform detectionPoint;
    public LayerMask playerLayer;
    public Transform attackPoint;
}

public struct EnemyTransfromEntry
{
    public Enemy_Type key;
    public EnemyTransform transform;
}

public class Enemy_Transform_Map : MonoBehaviour
{
    public static Enemy_Transform_Map Instance;

    [SerializeField]
    private List<EnemyTransfromEntry> entries = new List<EnemyTransfromEntry>();
    private Dictionary<Enemy_Type, EnemyTransform> transformMap = new Dictionary<Enemy_Type, EnemyTransform>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            foreach (EnemyTransfromEntry entry in entries)
            {
                transformMap[entry.key] = entry.transform;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public EnemyTransform GetTransform(Enemy_Type type)
    {
        return transformMap[type];
    }
}
