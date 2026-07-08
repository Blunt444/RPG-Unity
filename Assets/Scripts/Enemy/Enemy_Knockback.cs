using System.Collections;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemyMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        StartCoroutine(knockBackCounter(direction, knockbackForce, knockbackTime, stunTime));
    }

    private IEnumerator knockBackCounter(Vector2 direction, float maxForce, float duration, float stunTime)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float currentForce = Mathf.Lerp(maxForce, 0f, elapsed / duration);

            rb.linearVelocity = currentForce * direction;
            yield return null;
        }
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(stunTimeCounter(stunTime));
    }

    private IEnumerator stunTimeCounter(float stunTime)
    {
        yield return new WaitForSecondsRealtime(stunTime);

        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
