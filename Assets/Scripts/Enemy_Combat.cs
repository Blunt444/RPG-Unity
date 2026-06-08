using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().changeHealth(-damage);
    }
}
