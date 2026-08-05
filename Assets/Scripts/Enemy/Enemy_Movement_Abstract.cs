using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy_Movement_Abstract : MonoBehaviour
{
    public EnemyState enemyState;
    public Transform player;
    public bool isChasingUncontrolled;
    public Enemy_Manager manager;
    public NavMeshAgent agent;

    public float attackCooldownTimer;
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
        rb.bodyType = RigidbodyType2D.Kinematic;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = manager.attackRange * 0.8f;

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
                agent.ResetPath();
                ChangeState(EnemyState.Idle);
            }
            return;
        }

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState != EnemyState.Attacking)
        {
            CheckForPlayer();
        }

        if (enemyState == EnemyState.Attacking || enemyState == EnemyState.Idle)
        {
            agent.ResetPath();
        }
        else if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
    }

    public void HandleFlip()
    {
        if (player.position.x > transform.position.x && facingDirection == -1 ||
            player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeState(EnemyState newState)
    {

        if(enemyState == newState) return;

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

    public float GetAnimationLen()
    {
        RuntimeAnimatorController controller = anim.runtimeAnimatorController;

        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == "AttackLeft")
            {
                return clip.length;
            }
        }

        return 2.0f;
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

    public abstract void CheckForPlayer(); // this method will be purely for animator changing
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
