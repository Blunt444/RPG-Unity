using UnityEngine;

public class Enemy_Random_Spawn : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public BoxCollider2D spawnBoundary;
    public float minSec;
    public float maxSec;
    public float spawnCooldown;

    private float timer = 0;
    private float currentCooldown = 0;

    private void Start()
    {
        timer = Random.Range(minSec, maxSec);
    }

    private void FixedUpdate()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.fixedDeltaTime;
            return;
        }
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            timer = Random.Range(minSec, maxSec);
            currentCooldown = spawnCooldown;
        }

    }

    public void SpawnEnemy()
    {
        GameObject enemy = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
        Vector3 spawnPoint = GetRandomSpawnPoint();

        GameObject spawnedEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        Enemy_Movement movement = spawnedEnemy.GetComponentInChildren<Enemy_Movement>();
        Enemy_Health health = spawnedEnemy.GetComponentInChildren<Enemy_Health>();

        health.enemyContainer = spawnedEnemy;
        movement.SetChaseUncontrolled();
        movement.ChangeState(EnemyState.Chasing);

    }

    private Vector3 GetRandomSpawnPoint()
    {
        Bounds bounds = spawnBoundary.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector3(x, y, 0f);
    }

}