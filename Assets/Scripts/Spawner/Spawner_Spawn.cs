using UnityEngine;

public class Spawner_Spawn : MonoBehaviour
{

    public Transform playerInRadius;
    public int currSpawned;
    public int maxSpawned;

    private float timer;
    private Spawner_Manager manager;
    private float currBuffer = 0f;
    private float buffer = 2.5f;

    private void Awake()
    {
        manager = GetComponent<Spawner_Manager>();
        timer = Random.Range(manager.minSec, manager.maxSec + 1f);
    }

    private void FixedUpdate()
    {
        if (currSpawned >= maxSpawned)
        {
            return;
        }

        if (currBuffer > 0f)
        {
            currBuffer -= Time.fixedDeltaTime;

            return;
        }

        timer -= Time.fixedDeltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = Random.Range(manager.minSec, manager.maxSec);
            currBuffer = buffer;
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefab = manager.prefabs[Random.Range(0, manager.prefabs.Length)];

        Transform spawnPoint = manager.spawnPoints[Random.Range(0, manager.spawnPoints.Count)];

        GameObject enemy = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        enemy.GetComponent<Enemy_Manager>().spawnerHut = this;

        currSpawned++;
    }

    public void DecrementSpawnCount()
    {
        currSpawned--;
    }
}
