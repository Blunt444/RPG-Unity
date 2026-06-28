using System.Collections;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    public float speed;
    public float pauseDuration;

    private Rigidbody2D rb;
    private int currentPatrolIdx = 0;
    private Vector2 target;
    private Animator anim;
    private bool isPaused;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(SetPatrolPoints());
    }

    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector3)target - transform.position).normalized;

        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.y);
        }
        
        rb.linearVelocity = direction * speed;
        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(SetPatrolPoints());
        }
    }

    IEnumerator SetPatrolPoints()
    {
        isPaused = true;
        anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIdx++;
        if (currentPatrolIdx >= patrolPoints.Length) currentPatrolIdx = 0;
        target = patrolPoints[currentPatrolIdx];

        isPaused = false;
        anim.SetBool("isWalking", true);
    }
}
