using UnityEngine;

public abstract class Enemy_Combat_Abstract : MonoBehaviour
{

    public Enemy_Manager manager;

    protected void Awake()
    {
        manager = GetComponent<Enemy_Manager>();
    }

    public virtual void Attack()
    {

        Enemy_Movement_Abstract  movement = GetComponent<Enemy_Movement_Abstract >();
        if (movement.enemyState == EnemyState.Knockback)
        {
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(manager.attackPoint.position, manager.weaponRange, manager.playerLayer);
        if (hits.Length > 0)
        {
            if (hits[0].GetComponent<PlayerMovement>().isGuarding)
            {
                hits[0].GetComponent<PlayerMovement>().BreakGuard(manager.guardDamage, transform);
            }
            else
            {
                hits[0].GetComponent<PlayerHealth>().ChangeHealth(-manager.damage);
                hits[0].GetComponent<PlayerMovement>().Knockback(transform, manager.knockbackForce, manager.knockBackTime);
            }

            GetComponent<Enemy_Movement_Abstract>().attackCooldownTimer = manager.attackCooldownBuffer; 
        }
    }

    public void ResetAnimation()
    {


        Enemy_Movement_Abstract movement = GetComponent<Enemy_Movement_Abstract>();

        // Debug.Log("Enemy initial state" + movement.enemyState);

        if (movement.isChasingUncontrolled)
        {
            movement.ChangeState(EnemyState.Chasing);
        }
        else
        {
            movement.ChangeState(EnemyState.Idle);
        }

        // Debug.Log("Enemy Changed State" + movement.enemyState);
    }

    // public void OnDrawGizmosSelected()
    // {
    //     if (manager == null || manager.attackPoint == null) return;

    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(manager.attackPoint.position, manager.weaponRange);
    // }
}
