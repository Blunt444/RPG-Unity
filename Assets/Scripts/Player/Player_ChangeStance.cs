using System.Collections;
using UnityEngine;

public class Player_ChangeStance : MonoBehaviour
{
    public Animator anim;


    private float transistionTime = 0.5f;
    private float waitForFadeOut = 1.0f;
    private bool isTransistioning = false;

    void Update()
    {
        if (Input.GetButtonDown("ToggleStance") && !isTransistioning)
        {
            if (StanceManager.Instance.isSwitchingStanceAllowed())
                StartCoroutine(AnimationTransistion());
        }
    }

    private IEnumerator AnimationTransistion()
    {
        isTransistioning = true;
        anim.Play("FadeIn");
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(transistionTime);

        StanceManager.Instance.ChangeStance();

        anim.Play("FadeOut");
        Time.timeScale = 1;

        yield return new WaitForSecondsRealtime(waitForFadeOut);

        isTransistioning = false;
    }
}
