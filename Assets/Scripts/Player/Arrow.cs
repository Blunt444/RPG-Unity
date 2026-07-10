using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn;
    public float speed;
    public int damage;
    public float knockBackForce;
    public float knockBackTime;
    public float stunTime;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public SpriteRenderer sr;
    public Sprite buriedSprite;
    public float dampSpeed;
    public float gravity = 9.8f;
    public float stopThreshold;
    public float tiltAngle;
    private bool isInAir = false;
    private bool isDestroyedStarted = false;

    public void Launch(Vector2 dir)
    {
        Debug.Log("Launch called with direction: " + dir);
        direction = dir;
        rb.linearVelocity = direction * speed;
        isInAir = true;

        RotateArrow();
    }

    private void Update()
    {
        if (isInAir && rb.linearVelocity.sqrMagnitude > 0.1f)
        {
            RotateArrow();
        }
    }

    private void FixedUpdate()
    {
        if (!isInAir) return;

        rb.linearVelocity += Vector2.down * gravity * Time.fixedDeltaTime;

        if (dampSpeed > 0)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, dampSpeed * Time.fixedDeltaTime);
        }

        if (rb.linearVelocity.magnitude <= stopThreshold)
        {
            StartCoroutine(BuryArrow());
        }
    }

    private IEnumerator BuryArrow()
    {
        if (isDestroyedStarted) yield break;
        isDestroyedStarted = true;
        isInAir = false;

        Vector2 finalVelocityDir = rb.linearVelocity.sqrMagnitude > 0.001f ? rb.linearVelocity.normalized : (Vector2)(transform.right);

        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        float targetAngle = Mathf.Atan2(finalVelocityDir.y, finalVelocityDir.x) * Mathf.Rad2Deg;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, targetAngle);

        float t = 0f;
        while (t < tiltAngle)
        {
            t += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, endRot, t / tiltAngle);
            yield return null;
        }
        transform.rotation = endRot;

        sr.sprite = buriedSprite;

        Destroy(gameObject, lifeSpawn);
    }

    private void RotateArrow()
    {
        float targetAngle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            collision.gameObject.GetComponent<Enemy_Health>().ChangeHealth(-damage);
            collision.gameObject.GetComponent<Enemy_Knockback>().Knockback(transform, knockBackForce, knockBackTime, stunTime);
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }
    private void AttachToTarget(Transform target)
    {
        isInAir = false;
        sr.sprite = buriedSprite;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.SetParent(target);

        DestroyArrow();
    }

    private void DestroyArrow()
    {
        if (isDestroyedStarted) return;
        isDestroyedStarted = true;

        Destroy(gameObject, lifeSpawn);
    }
}
