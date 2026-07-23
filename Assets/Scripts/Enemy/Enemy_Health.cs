using UnityEngine;

public class Enemy_Health : MonoBehaviour
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
            OnMonsterDefeated(manager.expReward);
            Destroy(gameObject);
        }
    }
}
