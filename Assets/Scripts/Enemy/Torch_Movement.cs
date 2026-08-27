using UnityEngine;

public class Enemy_Movement : Enemy_Movement_Abstract
{
    public override void Chase()
    {
        agent.speed = manager.speed;
        agent.SetDestination(player.position);

        HandleFlip();
    }

    public override void CheckForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        Collider2D[] hits = Physics2D.OverlapCircleAll(manager.detectionPoint.position, manager.playerDetectionRange, manager.playerLayer);

        if (hits.Length > 0 || isChasingUncontrolled)
        {

            AudioManager.Instance.PlayMusic(enemyBgMusic);

            if (attackCooldownTimer > 0)
            {
                ChangeState(EnemyState.Idle);
                return;
            }

            if (distanceToPlayer <= manager.attackRange)
            {
                //got confused with frame added method this is just for the attack animation
                ChangeState(EnemyState.Attacking);
            }
            else if (distanceToPlayer > manager.attackRange)
            {
                ChangeState(EnemyState.Chasing);
            }

        }
        else
        {
            agent.ResetPath();
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