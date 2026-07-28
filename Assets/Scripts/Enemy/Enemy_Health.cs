using UnityEngine;

public class Enemy_Health : MonoBehaviour, Damageable
{
    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;

    Enemy_Manager manager;

    private void Awake()
    {
        manager = GetComponent<Enemy_Manager>();
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
    }

    private void Die()
    {
        OnMonsterDefeated?.Invoke(manager.expReward);

        if (manager.spawnerHut != null)
        {
            manager.spawnerHut.DecrementSpawnCount();
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        ChangeHealth(-damageAmount);

        if (TryGetComponent<Enemy_Knockback>(out Enemy_Knockback knockback))
        {
            knockback.Knockback(attacker, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
        }
    }
}
