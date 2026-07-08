using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public LayerMask playerLayer;
    public float knockbackForce;
    public float knockBackTime;
    public int guardDamage;

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            if (hits[0].GetComponent<PlayerMovement>().isGuarding)
            {
                hits[0].GetComponent<PlayerMovement>().BreakGuard(guardDamage, transform);
                return;
            }
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, knockbackForce, knockBackTime);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
}
