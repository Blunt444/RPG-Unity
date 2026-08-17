using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public TMP_Text message;
    public Queue<(string text, int timer)> queue = new Queue<(string text, int timer)>();
    private bool isDisplaying = false;

    private void OnEnable()
    {
        ShopManager.Message += HandleMessage;
        ArrowQuantityManager.Message += HandleMessage;
        QuestManager.Message += HandleMessage;
        PauseMenu.Message += HandleMessage;
    }
    private void OnDisable()
    {
        ShopManager.Message -= HandleMessage;
        ArrowQuantityManager.Message -= HandleMessage;
        QuestManager.Message -= HandleMessage;
        PauseMenu.Message -= HandleMessage;
    }

    private void HandleMessage(string text, int timer)
    {
        if (queue.Any(m => m.text == text))
        {
            return;
        }
        queue.Enqueue((text, timer));
    }

    private void Update()
    {
        if (!isDisplaying && queue.Count > 0)
        {
            StartCoroutine(DisplayMessages());
        }
    }

    private void Start()
    {
        message.gameObject.SetActive(false);
    }

    private IEnumerator DisplayMessages()
    {
        isDisplaying = true;

        var (text, timer) = queue.Peek();
        message.text = text;
        message.alpha = 1f;
        message.gameObject.SetActive(true);

        float elapsed = 0f;

        while (elapsed < timer)
        {
            elapsed += Time.unscaledDeltaTime;

            float progress = elapsed / timer;

            // Debug.Log(progress);

            // Debug.Log(elapsed);

            message.alpha = Mathf.Lerp(1f, 0f, progress);

            yield return null;
        }

        queue.Dequeue();
        // Debug.Log("Message time over");
        message.gameObject.SetActive(false);
        isDisplaying = false;
    }
}
