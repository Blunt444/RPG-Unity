using UnityEngine;

public class Spawner_Health : MonoBehaviour, Damageable
{
    private Spawner_Manager manager;

    private void Awake()
    {
        manager = GetComponent<Spawner_Manager>();
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

    public void Die()
    {
        GetComponent<Spawner_Destroyed>().OnDestroyed();
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
       ChangeHealth(-damageAmount);
    }
}
