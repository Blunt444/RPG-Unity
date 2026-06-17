using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

  public static StatsManager Instance;
  public TMP_Text healthText;
  public PlayerHealth playerHealth;

  [Header("Combat Stats")]
  public int damage;
  public float weaponRange;
  public float knockbackForce;
  public float knockbackTime;
  public float stunTime;
  public float attackCooldown;
  public float attackCooldownTimer;

  [Header("Movement Stats")]
  public int speed;

  [Header("Health Stats")]
  public int maxHealth;
  public int currentHealth;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void UpdateMaxHealth(int amount)
  {
    maxHealth += amount;
    playerHealth.ChangeHealth(amount);
    healthText.text = "HP: " + currentHealth + "/" + maxHealth;
  }
}
