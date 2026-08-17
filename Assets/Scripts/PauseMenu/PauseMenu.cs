using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isOpen;
    public CanvasGroup canvas;
    private void OnEnable()
    {
        PauseButton.onClick += Response;
    }

    private void OnDisable()
    {
        PauseButton.onClick -= Response;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        if (isOpen)
        {
            canvas.alpha = 0;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
            isOpen = false;
            Time.timeScale = 1;
        }
        else
        {
            canvas.alpha = 1;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
            isOpen = true;
            Time.timeScale = 0;
        }
    }

    private void Response(PauseButtonAction action)
    {
        switch (action)
        {
            case PauseButtonAction.Exit:
                ExitToMainMenu();
                return;
            case PauseButtonAction.Resume:
                TogglePauseMenu();
                return;
            case PauseButtonAction.Save:
                SaveManager.Instance.SaveGame();
                return;
            default:
                return;
        }
    }

    private void ExitToMainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(ExitCoroutine());
    }

    private IEnumerator ExitCoroutine()
    {
        yield return null;
        Debug.Log("Clean");
        GameManager.Instance.CleanUpPersistentObject();
        Debug.Log("Exit");
        SceneManager.LoadScene("MainMenu");
    }
}


[Serializable]
public enum PauseButtonAction
{
    Resume,
    Save,
    Exit
}