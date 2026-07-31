using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour, Damageable
{
    public int currentHit = 0;
    public int MaxHit = 0;

    private Sprite stump;
    private Animator anim;
    private SpriteRenderer sr;

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        currentHit++;
        Shake();
        if (currentHit >= MaxHit)
        {
            Die();
        }
    }

    public void Die()
    {
        anim.enabled = false;
        sr.sprite = stump;

        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            collider.pathCount = 0;
        }

        this.enabled = false;
    }

    public void Shake()
    {

    }

    private void Start()
    {
        if (TreeManager.Instance == null)
        {
            Destroy(gameObject);
            return;
        }

        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        TreeOverrides tree = TreeManager.Instance.RandomTreeVariant();

        stump = tree.choppedSprite[Random.Range(0, tree.choppedSprite.Length)];
        anim.runtimeAnimatorController = tree.overrider;

        anim.Play("TreeSway", -1, Random.Range(0f, 1f));
        anim.speed = Random.Range(0.85f, 1.15f);
    }
}
