using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int facingDirection;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isShooting;
    public bool isGuarding = false;
    public Player_Bow playerBow;

    private float currentDamp = 1.0f;
    private PlayerState playerState;
    private bool isKnockedBack;
    private Player_Combat playerCombat;
    private float currentGuardCooldown;

    void Awake()
    {
        playerState = PlayerState.Idle;
        playerCombat = GetComponent<Player_Combat>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Slash") && playerState != PlayerState.Attacking && playerCombat.enabled && !isGuarding)
        {
            playerCombat.Attack();
        }
        else if (Input.GetButtonDown("Guard") && !isGuarding && currentGuardCooldown <= 0)
        {
            ActivateGuard();
        }
        else if (Input.GetButtonUp("Guard") && isGuarding)
        {
            DeactivateGuard();
        }

        if (currentGuardCooldown > 0)
        {
            currentGuardCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (isShooting || isGuarding)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isKnockedBack)
        {
            return;
        }

        if (playerState == PlayerState.Attacking && StatsManager.Instance.attackCooldownTimer > 0)
        {
            StatsManager.Instance.attackCooldownTimer -= Time.deltaTime;
            return;
        }

        if(!playerCombat.enabled && playerBow.gameObject.activeInHierarchy)
        {
            playerBow.HandleAiming();
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (playerCombat.enabled || !playerBow.gameObject.activeInHierarchy)
        {
            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            ChangeState(PlayerState.Running);
        }
        else
        {
            ChangeState(PlayerState.Idle);
        }
        updateSpeed(horizontal, vertical);
    }

    public void updateSpeed(float horizontal, float vertical)
    {


        Vector2 moveInput = new Vector2(horizontal, vertical);

        if (playerCombat.enabled || !playerBow.gameObject.activeInHierarchy)
        {
            rb.linearVelocity = moveInput * StatsManager.Instance.speed;
            return;
        }

        Vector2 aimDirection = playerBow.aimDirection.normalized;
        Vector2 normMoveInput = moveInput.normalized;

        float dot = Vector2.Dot(normMoveInput, aimDirection);

        Debug.Log($"move:{normMoveInput} aim:{aimDirection} dot:{dot}");

        float targetDamp = (dot < -0.3f) ? StatsManager.Instance.speedDamp : 1.0f;

        currentDamp = Mathf.Lerp(currentDamp, targetDamp, Time.fixedDeltaTime * 10f);

        rb.linearVelocity = moveInput * StatsManager.Instance.speed * currentDamp;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void ActivateGuard()
    {
        isGuarding = true;
        ChangeState(PlayerState.Guard);
    }

    public void BreakGuard(int amount, Transform attacker)
    {

        StatsManager.Instance.currentGuardHit += amount;

        if (StatsManager.Instance.currentGuardHit >= StatsManager.Instance.maxGuardHitNegate)
        {
            StatsManager.Instance.currentGuardHit = 0;

            currentGuardCooldown = StatsManager.Instance.maxGuardCooldown;

            DeactivateGuard();

            TriggerBreakGuardKnockback(attacker);
        }
    }

    public void TriggerBreakGuardKnockback(Transform attacker)
    {
        isKnockedBack = true;
        Vector2 dir = (transform.position - attacker.position).normalized;

        float maxForce = 10;
        float duration = 0.15f;

        StartCoroutine(knockBackCounter(dir, maxForce, duration));
    }

    private IEnumerator knockBackCounter(Vector2 dir, float maxForce, float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float currentForce = Mathf.Lerp(maxForce, 0f, elapsed / duration);
            rb.linearVelocity = dir * currentForce;

            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }

    public void DeactivateGuard()
    {
        isGuarding = false;
        ChangeState(PlayerState.Idle);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        StartCoroutine(knockBackCounter(direction, force, stunTime));
    }


    public void ChangeState(PlayerState newState)
    {
        if (playerState == PlayerState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (playerState == PlayerState.Running)
        {
            anim.SetBool("isRunning", false);
        }
        else if (playerState == PlayerState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        else if (playerState == PlayerState.Shooting)
        {
            anim.SetBool("isShooting", false);
        }
        else if (playerState == PlayerState.Guard)
        {
            anim.SetBool("isGuarding", false);
        }

        playerState = newState;

        if (newState == PlayerState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (newState == PlayerState.Running)
        {
            anim.SetBool("isRunning", true);
        }
        else if (newState == PlayerState.Attacking)
        {
            anim.SetInteger("attackVariant", Random.Range(0, 2));
            anim.SetBool("isAttacking", true);
        }
        else if (newState == PlayerState.Shooting)
        {
            anim.SetBool("isShooting", true);
        }
        else if (newState == PlayerState.Guard)
        {
            anim.SetBool("isGuarding", true);
        }

    }
}

public enum PlayerState
{
    Attacking,
    Idle,
    Running,
    Shooting,
    Guard,
}