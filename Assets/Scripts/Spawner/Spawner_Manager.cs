using System.Collections.Generic;
using UnityEngine;

public class Spawner_Manager : MonoBehaviour
{
    public Spawner_Difficulty_Type type;
    public int currentHealth;
    public int maxHealth;
    public List<Transform> spawnPoints;
    public GameObject[] prefabs;
    public float minSec;
    public float maxSec;
    public float shakeDuration;
    public float shakeForce;
    public string id;

    public bool isDead = false;

    private void Awake()
    {
        type = Spawner_Difficulty.Instance.GetRandomDifficulty();
        id = Id.CreateId(transform.position);
        GetAndSetStat();
        GetSpawnPoints();
        Spawner_Difficulty.Instance.spawners.Add(this);
    }

    private void Start()
    {

    }

    public void GetAndSetStat()
    {
        Spawner_Stat stat = Spawner_Difficulty.Instance.GetStat(type);

        this.currentHealth = stat.maxHealth;
        this.maxHealth = stat.maxHealth;
        this.prefabs = stat.prefabs;
        this.minSec = stat.minSec;
        this.maxSec = stat.maxSec;
        this.shakeDuration = stat.shakeDuration;
        this.shakeForce = stat.shakeForce;
    }

    private void GetSpawnPoints()
    {
        spawnPoints.Add(transform.Find("SpawnPoint1"));
        spawnPoints.Add(transform.Find("SpawnPoint2"));
        spawnPoints.Add(transform.Find("SpawnPoint3"));
        spawnPoints.Add(transform.Find("SpawnPoint4"));
    }

}
