using TMPro;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    public TMP_Text upgradeCost;
    public RectTransform panel;

    public static SkillInfo Instance;

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
    }

    public void SetPanelPosition(Vector2 mouseTransfrom)
    {
        Vector2 offset = new Vector2(30f, -15f);

        panel.position = mouseTransfrom + offset;
    }
}
