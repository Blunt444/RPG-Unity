using System;
using System.Collections;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float radius = 2f;
    public float reachTime;
    public float detonateTime;
    public Vector2 playerPos;
    public int damageAmount = 1;
    [HideInInspector]
    public float knockbackTime;
    [HideInInspector]
    public float knockbackForce;
    public Animator anim;

    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Color fadeRed = new Color(1f, 0f, 0f, 0.2f);
    [SerializeField]
    private Color brightRed = new Color(1f, 0f, 0f, 0.8f);
    [SerializeField]
    private float startBlinkSpeed = 4f;
    [SerializeField]
    private float maxBlinkSpeed = 20f;
    private bool isDetonating = false;

    public void StartDetonation(Vector2 playerPos)
    {

        if (isDetonating) return;
        isDetonating = true;

        this.playerPos = playerPos;

        if (sr == null) Destroy(gameObject);

        sr.gameObject.SetActive(false);

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < reachTime)
        {

            elapsed += Time.deltaTime;

            float progress = elapsed / reachTime;

            transform.position = Vector3.Lerp(startPos, playerPos, progress);

            yield return null;
        }

        transform.position = playerPos;

        sr.gameObject.SetActive(true);

        sr.transform.position = transform.position;
        sr.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);

        StartCoroutine(Detonate());

    }

    private IEnumerator Detonate()
    {
        float elapsed = 0f;
        float blinkTimer = 0f;

        while (elapsed < detonateTime)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / detonateTime;

            float currentBlinSpeed = Mathf.Lerp(startBlinkSpeed, maxBlinkSpeed, progress);
            blinkTimer += Time.deltaTime * currentBlinSpeed;

            if (progress > 0.95) anim.Play("Explosion2");


            float blink = Mathf.PingPong(blinkTimer, 1f);
            sr.color = Color.Lerp(fadeRed, brightRed, blink);

            yield return null;
        }

        CheckPlayerInBlastRadius();
    }

    private void CheckPlayerInBlastRadius()
    {
        try
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

            if (hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.TryGetComponent<Damageable>(out Damageable target))
                    {
                        target.TakeDamage(damageAmount, transform);
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Debug.Log(e.Message);
        }
        finally
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sr.transform.position, radius);
    }

    private void OnValidate()
    {
        if (sr != null)
        {
            sr.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
        }
    }
}
