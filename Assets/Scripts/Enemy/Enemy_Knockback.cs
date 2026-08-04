using System.Collections;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemyMovement;
    private Enemy_Manager manager;

    void Start()
    {
        manager = GetComponent<Enemy_Manager>();
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }

    public void Knockback(Transform forceTransform, float knockbackForce, float knockbackTime, float stunTime)
    {

        float effectiveKnockback = knockbackTime * (1f - Mathf.Clamp01(manager.knockBackTimeResistance));
        float effectiveStunt = stunTime * (1f - Mathf.Clamp01(manager.stuntResistance));

        if (effectiveKnockback <= 0f && effectiveStunt <= 0f)
        {
            return;
        }

        enemyMovement.ChangeState(EnemyState.Knockback);
        Vector2 direction = (transform.position - forceTransform.position).normalized;

        StopAllCoroutines();

        if (effectiveKnockback <= 0f && effectiveStunt > 0f)
        {
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(stunTimeCounter(effectiveStunt));
        }
        else
        {
            StartCoroutine(knockBackCounter(direction, knockbackForce, effectiveKnockback, effectiveStunt));
        }

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
        
        enemyMovement.agent.ResetPath();

        if (stunTime > 0f)
            StartCoroutine(stunTimeCounter(stunTime));
        else
            enemyMovement.ChangeState(EnemyState.Idle);
    }

    private IEnumerator stunTimeCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);

        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
