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
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj == null) return;

        GameObject dynamite = Instantiate(TntPrefab, transform.position, Quaternion.identity);

        if (playerObj == null)
        {
            Destroy(dynamite);
            return;
        }

        dynamite.GetComponent<Dynamite>().StartDetonation(GameObject.FindGameObjectWithTag("Player").transform.position);

        dynamite.GetComponent<Dynamite>().knockbackTime = manager.knockBackTime;
        dynamite.GetComponent<Dynamite>().knockbackForce = manager.knockbackForce;
    }

}
