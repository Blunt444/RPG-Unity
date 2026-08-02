using UnityEngine;

public abstract class Enemy_Movement_Abstract : MonoBehaviour
{
    public EnemyState enemyState;
    public Transform player;
    public bool isChasingUncontrolled;
    public Enemy_Manager manager;

    protected float attackCooldownTimer;
    protected int facingDirection = -1;
    protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField]
    protected Transform healthCanvas;
    [SerializeField]
    protected Vector3 healthCanvasOffset;


    protected virtual void Awake()
    {
        manager = GetComponent<Enemy_Manager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else Destroy(gameObject);
    }

    public virtual void Update()
    {
        if (enemyState == EnemyState.Knockback)
        {
            return;
        }

        if (!player.gameObject.activeInHierarchy)
        {
            if (enemyState != EnemyState.Idle)
            {
                rb.linearVelocity = Vector2.zero;
                ChangeState(EnemyState.Idle);
            }
            return;
        }

        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (enemyState == EnemyState.Chasing || isChasingUncontrolled)
        {
            Chase();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
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

    public void SetChaseUncontrolled()
    {
        isChasingUncontrolled = true;
    }

    public void ResetChaseUncontrolled()
    {
        isChasingUncontrolled = false;
    }

    private void LateUpdate()
    {
        if (healthCanvas == null) return;
        healthCanvas.position = transform.position + healthCanvasOffset;
    }

    private void OnValidate()
    {
        if (healthCanvas == null) return;
        healthCanvas.position = transform.position + healthCanvasOffset;
    }

    public abstract void CheckForPlayer();
    public abstract void OnDrawGizmosSelected();
    public abstract void Chase();
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Knockback,
}
