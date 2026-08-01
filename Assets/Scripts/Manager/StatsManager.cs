using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
  public static StatsManager Instance;

  [Header("Combat Stats")]
  public int damage;
  public float weaponRange;
  public float knockbackForce;
  public float knockbackTime;
  public float stunTime;
  public float attackCooldown;
  public float attackCooldownTimer;
  public int currentGuardHit;
  public int maxGuardHitNegate;
  public float maxGuardCooldown;

  [Header("Movement Stats")]
  public int speed;

  [Header("Archer Stats")]
  public float speedDamp;

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
  private void OnValidate()
  {
    GameObject obj = GameObject.FindGameObjectWithTag("Player");
    if (obj == null) return;
    obj.GetComponent<Player_Combat>().OnDrawGizmosSelected();
  }

}
