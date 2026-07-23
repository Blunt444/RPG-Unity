using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    private float attackCooldownTimer;
    private int facingDirection = -1;
    private Animator anim;
    private EnemyState enemyState;
    private Rigidbody2D rb;
    private Transform player;

    public Enemy_Manager manager;

    private void Awake()
    {
        manager = GetComponent<Enemy_Manager>();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }
    public void Update()
    {
        if (enemyState == EnemyState.Knockback)
        {
            return;
        }

        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    public void Chase()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * manager.speed;
    }
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(manager.detectionPoint.position, manager.playerDetectionRange, manager.playerLayer);

        if (hits.Length > 0)
        {
            player = hits[0].transform;
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

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(manager.detectionPoint.position, manager.playerDetectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, manager.attackRange);
    }

    public void ChangeState(EnemyState newState)
    {
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        else if (enemyState == EnemyState.Knockback)
        {
            anim.SetBool("isKnocked", false);
        }

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else if (enemyState == EnemyState.Knockback)
        {
            anim.SetBool("isKnocked", true);
        }
    }

}


public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback,
    ChasePlayerUncontrolled
}