using UnityEngine;

public class Tnt_Movement : Enemy_Movement_Abstract
{
    [SerializeField]
    private float safeDistance;
    [SerializeField]
    private float maxTimeToFindSafeDistance;

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

    public void FindASafeDistance()
    {
        isFindingASafeDistance = true;
        float currTimeToFindSafeDistance = maxTimeToFindSafeDistance;

        ChangeState(EnemyState.Chasing);

        while (currTimeToFindSafeDistance > 0)
        {
            currTimeToFindSafeDistance -= Time.deltaTime;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, safeDistance, manager.playerLayer);

            if (hits.Length > 0)
            {
                
            }
            else
            {
                break;
            }
        }

        isFindingASafeDistance = false;

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
