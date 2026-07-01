using UnityEngine;

public class ToggleSkillTree : MonoBehaviour
{
    public CanvasGroup skillCanvas;
    private bool skillTreeOpen = false;

    void Update()
    {
        if (Input.GetButtonDown("ToggleSkillTree"))
        {
            if (skillTreeOpen)
            {
                Time.timeScale = 1;
                skillCanvas.alpha = 0;
                skillCanvas.blocksRaycasts = false;
                skillCanvas.interactable = false;
                skillTreeOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                skillCanvas.alpha = 1;
                skillCanvas.interactable = true;
                skillCanvas.blocksRaycasts = true;
                skillTreeOpen = true;
            }
        }

    }
}
