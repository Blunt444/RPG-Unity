using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Spawner_Difficulty_Type
{
    easy,
    medium,
    hard
}

[Serializable]
public struct Spawner_Stat
{
    public int maxHealth;
    public float minSec;
    public float maxSec;
    public GameObject[] prefabs;
}

[Serializable]
public struct Spawner_Stat_Difficulty_Map
{
    public Spawner_Difficulty_Type type;
    public Spawner_Stat stat;
}

public class Spawner_Difficulty : MonoBehaviour
{
    public static Spawner_Difficulty Instance;
    

    [SerializeField]
    private List<Spawner_Stat_Difficulty_Map> typeStatList = new List<Spawner_Stat_Difficulty_Map>();
    private Dictionary<Spawner_Difficulty_Type, Spawner_Stat> statMap = new Dictionary<Spawner_Difficulty_Type, Spawner_Stat>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            foreach (Spawner_Stat_Difficulty_Map entry in typeStatList)
            {
                statMap[entry.type] = entry.stat;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Spawner_Difficulty_Type GetRandomDifficulty()
    {
        return (Spawner_Difficulty_Type)UnityEngine.Random.Range(0, Enum.GetValues(typeof(Spawner_Difficulty_Type)).Length);
    }

    public Spawner_Stat GetStat(Spawner_Difficulty_Type type)
    {
        return statMap[type];
    }
}