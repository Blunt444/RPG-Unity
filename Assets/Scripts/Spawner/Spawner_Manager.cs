using UnityEngine;

public class Spawner_Manager : MonoBehaviour
{
    public Spawner_Difficulty_Type type;
    public int currentHealth;
    public int maxHealth;
    public Transform[] spawnPoints;
    public GameObject[] prefabs;
    public float minSec;
    public float maxSec;

    private void Awake()
    {
        type = GetComponent<Spawner_Difficulty>().GetRandomDifficulty();
        GetAndSetStat();

    }

    private void GetAndSetStat()
    {
        Spawner_Stat stat = GetComponent<Spawner_Difficulty>().GetStat(type);

        this.currentHealth = stat.maxHealth;
        this.maxHealth = stat.maxHealth;
        this.prefabs = stat.prefabs;
        this.minSec = stat.minSec;
        this.maxSec = stat.maxSec;
    }

}
