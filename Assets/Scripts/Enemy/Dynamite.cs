using System.Collections;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float radius = 2f;
    public float detonateTime;
    public Vector2 playerPos;
    public int damageAmount = 1;

    [SerializeField]
    private LayerMask playerLayer;

    public void StartDetonation(Vector2 playerPos)
    {
        this.playerPos = playerPos;
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < detonateTime)
        {

            elapsed += Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, playerPos, elapsed / detonateTime);

            yield return null;
        }
        CheckPlayerInBlastRadius();
    }

    private void CheckPlayerInBlastRadius()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, playerLayer);

        if (hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                if (hit.TryGetComponent<Damageable>(out Damageable taregt))
                {
                    taregt.TakeDamage(damageAmount, transform);
                }
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
