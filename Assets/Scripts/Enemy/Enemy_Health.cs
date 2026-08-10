using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : MonoBehaviour, Damageable
{
    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public GameObject enemyContainer;
    public static event Action<Enemy_Type> OnEnemyKilled;


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
        OnEnemyKilled?.Invoke(manager.enemyType);

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
