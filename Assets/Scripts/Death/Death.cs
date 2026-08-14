using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    public Sprite Skull;
    public Animator anim;
    public SpriteRenderer sr;
    private int timeToDecay = 2;

    public void Setup(int timeToDecay)
    {
        this.timeToDecay = timeToDecay;
        anim.Play("SkullDrop");
    }

    public void OnSkullDropFinished()
    {
        sr.sprite = Skull;
        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        yield return new WaitForSeconds(timeToDecay);

        anim.Play("SkullDecay");
    }

    public void DeleteGameObject()
    {
        Destroy(gameObject);
    }
}
