using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    public TMP_Text upgradeCost;
    public RectTransform panel;
    public Transform prerequestPanel;
    public GameObject prerequestTextPrefab;
    public RectTransform costRectTransform;
    public RectTransform canvasRectTransform;
    public Camera uiCamera;

    public static SkillInfo Instance;


    private float costDefaultX = -62f;
    private float costDefaultY = 0f;
    private float costTransformedY = -86.9f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            panel.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPanelState(bool isActive)
    {
        if (panel == null) return;
        panel.gameObject.SetActive(isActive);
    }

    public void SetCostText(int amount)
    {
        upgradeCost.text = amount.ToString();
    } // make this method to change to red and green depending on this cost and available points

    public void SetPanelPosition(Vector2 mouseScreenPos)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            mouseScreenPos,
            uiCamera,
            out localPoint
        );

        float halfWidth = (panel.rect.width * panel.localScale.x) / 2f;
        float halfHeight = (panel.rect.height * panel.localScale.y) / 2f;

        Vector2 offset = new Vector2(halfWidth + 10f, -(halfHeight + 10f));

        panel.anchoredPosition = localPoint + offset;
    }

    public void SetCostYPos(bool isPrerequestTextEmpty)
    {
        Debug.Log(isPrerequestTextEmpty);
        if (isPrerequestTextEmpty)
        {
            costRectTransform.anchoredPosition = new Vector2(costDefaultX, costDefaultY);
        }
        else
        {
            costRectTransform.anchoredPosition = new Vector2(costDefaultX, costTransformedY);
        }
    }

    public void ShowPrerequestOnHover(List<ReslovedPrerequisiteSkillSlots> reslovedPrerequisiteSkillSlot)
    {
        foreach (ReslovedPrerequisiteSkillSlots rs in reslovedPrerequisiteSkillSlot)
        {
            var newTextBlock = Instantiate(prerequestTextPrefab, prerequestPanel);
            PrerequestTextBlock textBlock = newTextBlock.GetComponent<PrerequestTextBlock>();
            textBlock.SetPrerequestSkillName(rs.slot.skillSO.skillName, rs.requiredLevel);
        }
    }

    public void ClearPrerequestOnExitHover()
    {
        foreach (Transform child in prerequestPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
