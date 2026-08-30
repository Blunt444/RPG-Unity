using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isOpen;
    public CanvasGroup canvas;
    public bool isSaveClicked = false;
    public int messageTime = 4;
    public Transform settingPanel;
    public Transform buttonsPanel;
    public static event Action<string, int> Message;
    private void OnEnable()
    {
        PauseButton.onClick += Response;
    }

    private void Start()
    {
        settingPanel.gameObject.SetActive(false);
        buttonsPanel.gameObject.SetActive(true);
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
        if (isSaveClicked) return;

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

    private void Response(PauseButtonAction action, PauseButton pauseButton)
    {
        if (isSaveClicked) return;

        switch (action)
        {
            case PauseButtonAction.Exit:
                ExitToMainMenu();
                return;
            case PauseButtonAction.Resume:
                TogglePauseMenu();
                return;
            case PauseButtonAction.Save:
                // isSaveClicked = true;
                // pauseButton.textBox.text = "Saving..";
                // bool saved = SaveManager.Instance.SaveGame();
                // pauseButton.textBox.text = "Save";
                // if (saved)
                // {
                //     Message?.Invoke("Game Saved", messageTime);
                // }
                // isSaveClicked = false;
                StartCoroutine(SaveCoroutine(pauseButton));
                return;
            case PauseButtonAction.Setting:
                if(settingPanel != null)
                {
                    settingPanel.gameObject.SetActive(true);
                    buttonsPanel.gameObject.SetActive(false);
                }
                return;
            case PauseButtonAction.Back:
                if(settingPanel != null)
                {
                    settingPanel.gameObject.SetActive(false);           // I didn't want to implement stack cause there is only a single back button so far
                    buttonsPanel.gameObject.SetActive(true);            // In future if there is 2 or more back i will implement stack
                }
                return;
            default:
                return;
        }
    }

    private IEnumerator SaveCoroutine(PauseButton pauseButton)
    {
        isSaveClicked = true;
        SaveManager.Instance.isNewGame = false;

        pauseButton.textBox.text = "Saving..";

        bool saved = SaveManager.Instance.SaveGame();

        yield return new WaitForSecondsRealtime(2);

        if (saved)
        {
            Message?.Invoke("Game Saved.", messageTime);
        }
        else
        {
            Message?.Invoke("Faild To Save Game.", messageTime);
        }

        pauseButton.textBox.text = "Save";
        isSaveClicked = false;
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
    Exit,
    Setting,
    Back
}