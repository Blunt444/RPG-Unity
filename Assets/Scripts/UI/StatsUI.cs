using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlot;
    public CanvasGroup StatsCanvas;

    private bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }
    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))
        {
            if (statsOpen)
            {
                Time.timeScale = 1;
                UpdateAllStats();
                StatsCanvas.alpha = 0;
                StatsCanvas.blocksRaycasts = false;
                statsOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                UpdateAllStats();
                StatsCanvas.alpha = 1;
                StatsCanvas.blocksRaycasts = true;
                statsOpen = true;
            }
        }
    }

    public void UpdateDamageStatsUI()
    {
        statsSlot[0].GetComponentInChildren<TMP_Text>().text = "Strength " + StatsManager.Instance.damage;
    }
    public void UpdateSpeedStatsUI()
    {
        statsSlot[1].GetComponentInChildren<TMP_Text>().text = "Speed " + StatsManager.Instance.speed;
    }

    public void UpdateAllStats()
    {
        UpdateDamageStatsUI();
        UpdateSpeedStatsUI();
    }
}
