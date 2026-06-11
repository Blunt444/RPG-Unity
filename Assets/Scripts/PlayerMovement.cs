using System.Collections;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public int facingDirection = 1;
    public Rigidbody2D rb;
    public Animator anim;

    private PlayerState playerState;
    private bool isKnockedBack;

    void Start()
    {
        playerState = PlayerState.Idle;
    }

    void FixedUpdate()
    {

        if (isKnockedBack)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            ChangeState(PlayerState.Running);
        }
        else
        {
            ChangeState(PlayerState.Idle);
        }

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }

    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;

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
            anim.SetBool("isAttacking", true);
        }

    }
}

public enum PlayerState
{
    Attacking,
    Idle,
    Running
}