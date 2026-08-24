using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour, Damageable
{
    public int timeToDecay = 7;
    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public GameObject enemyContainer;
    public static event Action<Enemy_Type, string> OnEnemyKilled;


    [SerializeField]
    private Image fillImage;
    private Enemy_Manager manager;

    private void Awake()
    {
        manager = GetComponent<Enemy_Manager>();

        if (enemyContainer == null)
        {
            enemyContainer = transform.root.gameObject;
        }
    }

    public void ChangeHealth(int amount)
    {
        manager.currentHealth += amount;

        if (manager.currentHealth > manager.maxHealth)
        {
            manager.currentHealth = manager.maxHealth;
        }
        else if (manager.currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthUI();

    }

    private void Die()
    {
        OnMonsterDefeated?.Invoke(manager.expReward);
        if (manager.spawnerHut == null)
            OnEnemyKilled?.Invoke(manager.enemyType, manager.id);

        int len = UnityEngine.Random.Range(manager.minDrop, manager.maxDrop + 1);

        for (int i = 0; i < len; i++)
        {
            if (manager.loots.Count == 0) break;

            int index = UnityEngine.Random.Range(0, manager.loots.Count);
            int quantity = UnityEngine.Random.Range(manager.minQuantity, manager.maxQuantity + 1);

            if (quantity <= 0) continue;

            Vector3 spawnPos = transform.position + (Vector3)(UnityEngine.Random.insideUnitCircle * 2f);
            Loot loot = Instantiate(manager.LootPrefab, spawnPos, Quaternion.identity).GetComponent<Loot>();
            loot.Initialize(manager.loots[index], quantity);

            manager.loots.RemoveAt(index);
        }

        Death death = Instantiate(manager.deathPrefab, transform.position, Quaternion.identity);
        death.Setup(timeToDecay);

        if (manager.spawnerHut != null)
        {
            manager.spawnerHut.DecrementSpawnCount();
        }

        Destroy(enemyContainer);
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        ChangeHealth(-damageAmount);

        if (TryGetComponent<Enemy_Knockback>(out Enemy_Knockback knockback))
        {
            knockback.Knockback(attacker, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }
    }

    public void UpdateHealthUI()
    {
        fillImage.fillAmount = (float)manager.currentHealth / manager.maxHealth;
    }
}
