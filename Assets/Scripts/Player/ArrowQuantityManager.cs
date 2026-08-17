using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ArrowQuantityManager : MonoBehaviour
{
    public static ArrowQuantityManager Instance;
    private int maxAmount;
    private int currentAmount;
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

    public int GetMaxArrowCount()
    {
        return maxAmount;
    }

    public bool SetQuantity(int amount)
    {
        currentAmount += amount;
        bool isArrowAdded = true;
        if (currentAmount > maxAmount)
        {
            currentAmount = maxAmount;
            isArrowAdded = false;

        }
        else if (currentAmount < 0)
        {
            currentAmount = 0;
            Message?.Invoke("Out of Arrow", messageTimer);
            isArrowAdded = false;
        }
        UpdateQuantityText();
        return isArrowAdded;
    }

    public void SetArrowData(int currAmount, int maxAmount)
    {
        currentAmount = currAmount;
        this.maxAmount = maxAmount;
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
