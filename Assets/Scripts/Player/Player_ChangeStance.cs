using System.Collections;
using UnityEngine;

public class Player_ChangeStance : MonoBehaviour
{
    public Player_Combat playerCombat;
    public Player_Bow playerBow;
    public Animator anim;


    private float transistionTime = 0.5f;
    private float waitForFadeOut = 1.0f;
    private bool isTransistioning = false;

    void Start()
    {
        playerCombat.enabled = true;
        playerBow.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("ToggleStance") && !isTransistioning)
        {
            StartCoroutine(AnimationTransistion());
        }
    }

    private IEnumerator AnimationTransistion()
    {
        isTransistioning = true;
        anim.Play("FadeIn");
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(transistionTime);

        playerCombat.enabled = !playerCombat.enabled;
        playerBow.gameObject.SetActive(!playerBow.gameObject.activeSelf);

        anim.Play("FadeOut");
        Time.timeScale = 1;

        yield return new WaitForSecondsRealtime(waitForFadeOut);

        isTransistioning = false;
    }
}
