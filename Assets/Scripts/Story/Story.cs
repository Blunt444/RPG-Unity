using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour, IPointerClickHandler
{
    public StoryContent storyContent = new StoryContent();
    public TMP_Text textBox;
    public TMP_Text writerBox;
    public int index = 0;
    public bool canBeSkipped = false;
    public bool isTyping = false;
    private Coroutine typeWriter;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isTyping)
        {
            canBeSkipped = true;
        }
        else
        {
            AdvanceNextLine();
        }
    }

    private IEnumerator ResetSkip()
    {
        yield return new WaitForSecondsRealtime(2);

        canBeSkipped = false;
    }

    private void Start()
    {
        textBox.text = "";
        writerBox.text = "";
        textBox.gameObject.SetActive(true);
        writerBox.gameObject.SetActive(true);

        StartText();
    }

    private void AdvanceNextLine()
    {
        index++;
        StartText();
    }

    private void StartText()
    {
        if (index >= storyContent.lines.Count)
        {
            SceneManager.LoadScene("Scene1");
            return;
        }
        if (typeWriter != null)
        {
            StopCoroutine(typeWriter);
            typeWriter = null;
        }
        typeWriter = StartCoroutine(TypeWrite());
    }

    private IEnumerator TypeWrite()
    {

        isTyping = true;
        canBeSkipped = false;

        float elapsed = 0f;
        bool cursorOn = false;
        float cursorTime = 0f;
        float cursorBlinkRate = 0.6f;

        if (storyContent.lines[index].writer != "")
        {
            writerBox.text = storyContent.lines[index].writer;
            writerBox.alpha = 0;
        }
        else
        {
            writerBox.text = "";
            writerBox.alpha = 0;
        }

        while (elapsed < 2f)
        {
            if (cursorTime >= cursorBlinkRate)
            {
                cursorTime = 0f;
                cursorOn = !cursorOn;
                textBox.text = cursorOn ? "|" : "";
            }
            elapsed += Time.unscaledDeltaTime;
            cursorTime += Time.unscaledDeltaTime;
            yield return null;
        }

        string line = storyContent.lines[index].line;
        string revealed = "";

        foreach (char c in line)
        {
            if (canBeSkipped)
            {
                revealed = line;
                textBox.text = line;
                break;
            }
            revealed += c;
            textBox.text = revealed + "|";
            yield return new WaitForSecondsRealtime(0.2f);
        }

        isTyping = false;
        canBeSkipped = false;

        writerBox.alpha = 1;

        cursorTime = 0f;

        while (true)
        {
            cursorTime += Time.unscaledDeltaTime;

            if (cursorTime >= cursorBlinkRate)
            {
                cursorTime = 0f;
                cursorOn = !cursorOn;
                textBox.text = revealed + (cursorOn ? "|" : "");
            }

            yield return null;
        }
    }

}


[Serializable]
public class StoryContent
{
    public List<StoryLine> lines;
}

[Serializable]
public class StoryLine
{
    public string line;
    public string writer;
}