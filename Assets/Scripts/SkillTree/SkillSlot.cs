using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public Image skillIcon;
    public TMP_Text skillLvlText;
    public Button skillButton;
    public int currentLevel;
    public bool isUnlocked;

    private void OnValidate()
    {
        if (skillSO != null && skillLvlText != null)
        {
            OnUpdate();
        }
    }
    private void OnUpdate()
    {
        skillIcon.sprite = skillSO.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLvlText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLvlText.text = "Locked";
            skillIcon.color = Color.grey;
        }

    }
}
