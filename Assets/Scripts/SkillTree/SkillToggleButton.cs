using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillToggleButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image button;
    public SkillCategory type;
    public void OnPointerDown(PointerEventData eventData)
    {
        SkillTreeManager.Instance.ShowSkills(type);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = button.color;
        color.a = 0.5f;
        button.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color color = button.color;
        color.a = 1f;
        button.color = color;
    }
}
