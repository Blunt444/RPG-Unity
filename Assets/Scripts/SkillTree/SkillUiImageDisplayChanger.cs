using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SkillUiImageDisplayChanger : MonoBehaviour
{
    public Image image;
    public Sprite combatSprite;
    public Sprite archerySprite;

    private SkillCategory currentCategory = SkillCategory.Combat;

    private void OnValidate()
    {
        UpdateSprite();
    }

    private void SetSprite(SkillCategory newCategory)
    {
        currentCategory = newCategory;
    }

    private void UpdateSprite()
    {
        if (image == null) return;

        switch (currentCategory)
        {
            case SkillCategory.Archery:
                image.sprite = archerySprite; break;
            default:
                image.sprite = combatSprite;
                break;
        }
    }
}
