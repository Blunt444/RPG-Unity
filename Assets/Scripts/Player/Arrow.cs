using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn;
    public float speed;

    void Start()
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, lifeSpawn);
    }
}
