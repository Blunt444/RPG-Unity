using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour, Damageable
{
    public int currentHit = 0;
    public int MaxHit = 0;

    private Sprite latestSprite;
    private Sprite stump;
    private Animator anim;
    private SpriteRenderer sr;
    private PolygonCollider2D polygonCollider2D;
    private Vector3 localPos;
    private bool isShaking = false;
    private bool isDead = false;

    private void Start()
    {
        if (TreeManager.Instance == null)
        {
            Destroy(gameObject);
            return;
        }

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        TreeOverrides tree = TreeManager.Instance.RandomTreeVariant();

        stump = tree.choppedSprite[Random.Range(0, tree.choppedSprite.Length)];
        anim.runtimeAnimatorController = tree.overrider;
        localPos = transform.localPosition;


        anim.Play("TreeSway", -1, Random.Range(0f, 1f));
        anim.speed = Random.Range(0.85f, 1.15f);
    }

    private void LateUpdate()
    {
        if (sr.sprite != latestSprite)
        {
            UpdateColliderShape(sr.sprite);
            latestSprite = sr.sprite;
        }
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        if(isDead) return;
        
        currentHit++;
        Shake();
        if (currentHit >= MaxHit)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.enabled = false;
        sr.sprite = stump;

        TreeManager.Instance.DropWood(transform);

        if (polygonCollider2D == null)
        {
            polygonCollider2D.pathCount = 0;
        }

        isDead = true;
    }

    private void Shake()
    {
        if (isShaking)
        {
            return;
        }

        isShaking = true;
        StartCoroutine(StartShaking());
    }

    private IEnumerator StartShaking()
    {
        float elapsed = 0f;
        while (elapsed < TreeManager.Instance.maxShakeTime)
        {
            elapsed += Time.deltaTime;

            float x = Random.Range(-1f, 1f) * TreeManager.Instance.shakeMagnitude;
            float y = Random.Range(-1f, 1f) * TreeManager.Instance.shakeMagnitude;

            transform.localPosition = localPos + new Vector3(x, y, 0);

            yield return null;
        }
        isShaking = false;
        transform.localPosition = localPos;
    }

    private void UpdateColliderShape(Sprite sprite)
    {
        if (polygonCollider2D == null || sprite == null) return;

        int shapeCount = sprite.GetPhysicsShapeCount();
        polygonCollider2D.pathCount = shapeCount;

        List<Vector2> path = new List<Vector2>();

        for (int i = 0; i < shapeCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider2D.SetPath(i, path);
        }
    }
}
