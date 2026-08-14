using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowQuantityManager : MonoBehaviour
{
    public static ArrowQuantityManager Instance;
    public int maxAmount;
    public int currentAmount;
    public TMP_Text quantityText;
    public CanvasGroup canvas;
    public float displayTime = 2f;
    public int messageTimer = 4;
    public static event Action<string, int> Message;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        SetQuantity(maxAmount);
        HideCanvas();
    }

    public void DisplayCanvas()
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }
    public void HideCanvas()
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    public int GetQuantity()
    {
        return currentAmount < 0 ? 0 : currentAmount;
    }

    public bool SetQuantity(int amount)
    {
        currentAmount += amount;
        if (currentAmount > maxAmount)
        {
            currentAmount = maxAmount;
            return false;

        }
        else if (currentAmount < 0)
        {
            currentAmount = 0;
            Message?.Invoke("Out of Arrow",messageTimer);
            return false;
        }
        UpdateQuantityText();
        return true;
    }

    private void UpdateQuantityText()
    {
        quantityText.text = "x " + (currentAmount < 0 ? 0 : currentAmount);
    }

    // private IEnumerator DisplayMessage()
    // {
    //     float elapsed = 0f;

    //     while(elapsed < )
    // }

}
