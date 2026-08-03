using UnityEngine;

public class Tnt_Movement : Enemy_Movement_Abstract
{
    public override void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * manager.speed;
    }

    public override void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(manager.detectionPoint.position, manager.playerDetectionRange, manager.playerLayer);

        if (hits.Length > 0)
        {
                
        }
    }

    public override void OnDrawGizmosSelected()
    {
        if (manager == null || manager.detectionPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(manager.detectionPoint.position, manager.playerDetectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, manager.attackRange);
    }
}
