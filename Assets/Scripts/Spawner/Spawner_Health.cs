using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner_Health : MonoBehaviour, Damageable
{
    private Spawner_Manager manager;
    private Vector3 originalLocalPos;
    private Coroutine shakeCoroutine;

    [SerializeField]
    private Image fillImage;


    private void Awake()
    {
        manager = GetComponent<Spawner_Manager>();
        originalLocalPos = transform.localPosition;
        UpdateHealthUI();
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

        UpdateHealthUI();
    }

    public void Die()
    {
        GetComponent<Spawner_Destroyed>().OnDestroyed();
        manager.isDead = true;
    }

    public void UpdateHealthUI()
    {
        fillImage.fillAmount = (float)manager.currentHealth / manager.maxHealth;
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
            float x = UnityEngine.Random.Range(-1f, 1f) * manager.shakeForce;
            float y = UnityEngine.Random.Range(-1f, 1f) * manager.shakeForce;

            transform.localPosition = originalLocalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalLocalPos;
        shakeCoroutine = null;
    }
}
