using UnityEngine;

public interface Damageable
{
    void TakeDamage(int damageAmount, Transform attacker);
}

public class Player_Combat : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask enemyLayer;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Attack()
    {
        playerMovement.ChangeState(PlayerState.Attacking);

        playerMovement.SetPlayerSpeedToZero();

        StatsManager.Instance.attackCooldownTimer = StatsManager.Instance.attackCooldown;

    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackPoint.position, StatsManager.Instance.attackBoxSize, enemyLayer);
        if (enemies.Length > 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.TryGetComponent<Damageable>(out Damageable target))
                {
                    target.TakeDamage(StatsManager.Instance.damage, transform);
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }
}
