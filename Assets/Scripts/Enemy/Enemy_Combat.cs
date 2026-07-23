using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{

    public Enemy_Manager manager;

    private void Awake()
    {
        manager = GetComponent<Enemy_Manager>();
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(manager.attackPoint.position, manager.weaponRange, manager.playerLayer);
        if (hits.Length > 0)
        {
            if (hits[0].GetComponent<PlayerMovement>().isGuarding)
            {
                hits[0].GetComponent<PlayerMovement>().BreakGuard(manager.guardDamage, transform);
                return;
            }
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-manager.damage);
            hits[0].GetComponent<PlayerMovement>().Knockback(transform, manager.knockbackForce, manager.knockBackTime);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(manager.attackPoint.position, manager.weaponRange);
    }
}
