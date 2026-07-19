using TMPro;
using UnityEngine;

public class PrerequestTextBlock : MonoBehaviour
{
    private TMP_Text prerequestSkillName;

    private void Awake()
    {
        prerequestSkillName = GetComponent<TMP_Text>();
    }

    public void SetPrerequestSkillName(string prerequestSkillName, int requiredLevel)
    {
        this.prerequestSkillName.text = prerequestSkillName + " : " + requiredLevel;
    }
}
