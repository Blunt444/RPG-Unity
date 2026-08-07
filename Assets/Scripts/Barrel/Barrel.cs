using System.Collections;
using UnityEngine;

public class Barrel : MonoBehaviour, Damageable
{
    public float radius = 2f;
    public float knockbackForce;
    public float knockbackTime;
    public bool gotHit = false;
    public int damageAmount = 2;
    public Sprite igniteFrame;
    public Sprite burningFrame;
    public Sprite explodeFrame;
    public float explodeTime;
    public Color fadeRed = new Color(1f, 0f, 0f, 0.2f);
    public Color brightRed = new Color(1f, 0f, 0f, 0.8f);
    public float startBlinkSpeed = 4f;
    public float maxBlinkSpeed = 20f;


    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private SpriteRenderer blastZone;

    private void Awake()
    {
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
        if (blastZone != null)
        {
            blastZone.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
            blastZone.gameObject.SetActive(false);
        }
    }

    private void OnValidate()
    {
        if (blastZone != null)
        {
            blastZone.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        if (gotHit)
        {
            return;
        }

        StartCoroutine(StartBlast());
    }

    private IEnumerator StartBlast()
    {
        float elapsed = 0f;
        float blinkTimer = 0f;
        gotHit = true;
        blastZone.gameObject.SetActive(true);

        while (elapsed < explodeTime)
        {
            elapsed += Time.deltaTime;
            yield return null;

            float progress = elapsed / explodeTime;

            float currentBlinkSpeed = Mathf.Lerp(startBlinkSpeed, maxBlinkSpeed, progress);
            blinkTimer += Time.deltaTime * currentBlinkSpeed;

            float blink = Mathf.PingPong(blinkTimer, 1f);

            Color color = Color.Lerp(fadeRed, brightRed, blink);
            blastZone.color = color;

            if (progress < 0.6)
            {
                sr.sprite = igniteFrame;
            }
            else if (progress < 0.9)
            {
                sr.sprite = burningFrame;
            }
            else
            {
                sr.sprite = explodeFrame;
            }
        }

        CheckForDamageble();

        Destroy(gameObject);
    }

    private void CheckForDamageble()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hit in hits)
        {

            if(hit.gameObject == gameObject) continue;

            if (hit.TryGetComponent<Damageable>(out Damageable target))
            {
                target.TakeDamage(damageAmount, transform);
            }
        }
    }

}
