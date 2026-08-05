using UnityEngine;

public class Tnt_Combat : Enemy_Combat_Abstract
{
    [SerializeField]
    private GameObject TntPrefab;
    public override void Attack()
    {
        Enemy_Movement_Abstract movement = GetComponent<Enemy_Movement_Abstract>();

        if (movement.enemyState == EnemyState.Knockback)
        {
            return;
        }

        movement.HandleFlip();

        ThrowDynamite();

        movement.attackCooldownTimer = manager.attackCooldownBuffer;
    }

    public void ThrowDynamite()
    {
        GameObject dynamite = Instantiate(TntPrefab, transform.position, Quaternion.identity);
        dynamite.GetComponent<Dynamite>().StartDetonation(GameObject.FindGameObjectWithTag("Player").transform.position);
    }

}
