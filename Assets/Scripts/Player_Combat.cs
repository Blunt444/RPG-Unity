using UnityEngine;

public class Player_Combat : MonoBehaviour
{

    public Transform attackPoint;
    public float weaponRange;
    public LayerMask enemyLayer;
    public int damage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Attack()
    {
        playerMovement.ChangeState(PlayerState.Attacking);

        playerMovement.attackCooldownTimer = playerMovement.attackCooldown;

    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-damage);
            enemies[0].GetComponent<Enemy_Knockback>().Knockback(transform, knockbackForce, knockbackTime, stunTime);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
