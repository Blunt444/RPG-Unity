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

    private bool isInAir = false;

    public void Launch(Vector2 dir)
    {
        Debug.Log("Launch called with direction: " + dir);
        direction = dir;
        rb.linearVelocity = direction * speed;
        isInAir = true;
        Destroy(gameObject, lifeSpawn);
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
        if (isInAir && dampSpeed > 0)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.fixedDeltaTime * dampSpeed);
        }
    }

    private void RotateArrow()
    {
        float targetAngle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
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
    public void AttachToTarget(Transform target)
    {
        isInAir = false;
        sr.sprite = buriedSprite;
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;

        transform.SetParent(target);
    }
}
