using System.Collections;
using UnityEngine;

public class Spawner_Health : MonoBehaviour, Damageable
{
    private Spawner_Manager manager;
    private Vector3 originalLocalPos;
    private Coroutine shakeCoroutine;


    private void Awake()
    {
        manager = GetComponent<Spawner_Manager>();
        originalLocalPos = transform.localPosition;
    }

    public void ChangeHealth(int amount)
    {
        manager.currentHealth += amount;

        if (manager.currentHealth > manager.maxHealth)
        {
            manager.currentHealth = manager.maxHealth;
        }
        else if (manager.currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GetComponent<Spawner_Destroyed>().OnDestroyed();
        manager.isDead = true;
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        ChangeHealth(-damageAmount);

        if (!manager.isDead)
            TriggerHitShake();
    }

    private void TriggerHitShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = originalLocalPos;
        }

        shakeCoroutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < manager.shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * manager.shakeForce;
            float y = Random.Range(-1f, 1f) * manager.shakeForce;

            transform.localPosition = originalLocalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalLocalPos;
        shakeCoroutine = null;
    }
}
