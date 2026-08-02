using UnityEngine;

public class Enemy_Movement : Enemy_Movement_Abstract
{
    public override void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * manager.speed;
    }
    
    public override void CheckForPlayer()
    {
        if (isChasingUncontrolled)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= manager.attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = manager.attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (distanceToPlayer > manager.attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }

            if (player.position.x > transform.position.x && facingDirection == -1 ||
                player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(manager.detectionPoint.position, manager.playerDetectionRange, manager.playerLayer);

        if (hits.Length > 0)
        {
            if (Vector2.Distance(transform.position, player.position) <= manager.attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = manager.attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > manager.attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
            if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }

        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
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