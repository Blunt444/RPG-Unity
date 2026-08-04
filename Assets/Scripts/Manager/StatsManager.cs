using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
  public static StatsManager Instance;

  [Header("Combat Stats")]
  public int damage;
  public Vector2 attackBoxSize = new Vector2(1.5f, 2.5f);
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
  private void OnDrawGizmos()
  {
    GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    if (playerObj == null) return;

    if (playerObj.TryGetComponent<Player_Combat>(out Player_Combat combat))
    {
      if (combat.attackPoint == null) return;

      Gizmos.color = Color.green;
      Gizmos.DrawWireCube(combat.attackPoint.position, attackBoxSize);
    }
  }

}
