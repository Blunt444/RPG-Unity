using System;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public MainMenuButton clickedButton;
    public List<MainMenuButton> allButtons = new List<MainMenuButton>();

    public string newScene = "Scene1";

    public Transform MainPanel;
    public Transform GuidePanel;
    public Transform PlayPanel;
    public Transform LoadPanel;
    public Transform LoadContent;
    public Transform LoadNoContent;
    public LoadBox LoadBoxPrefab;
    public Transform currentPanel;
    public Transform previousPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ButtonClicked(MainMenuButton button)
    {
        clickedButton = button;

        switch (button.action)
        {
            case ButtonAction.Guide:
                SetActivePanel(GuidePanel);
                return;
            case ButtonAction.Play:
                SetActivePanel(PlayPanel);
                return;
            case ButtonAction.Exit:
                QuitGame();
                return;
            case ButtonAction.NewGame:
                SceneManager.LoadScene(newScene);
                return;
            case ButtonAction.LoadGame:
                SetActivePanel(LoadPanel);
                return;
            case ButtonAction.Back:
                SetActivePanel(previousPanel != null ? previousPanel : MainPanel);
                return;
            default:
                return;
        }
    }

    public void ShowAllSaves()
    {
        List<string> files = SaveManager.Instance.GetAllSaves();

        if (files.Count <= 0)
        {
            LoadNoContent.gameObject.SetActive(true);
            LoadContent.gameObject.SetActive(false);
            return;
        }

        LoadNoContent.gameObject.SetActive(false);
        LoadContent.gameObject.SetActive(true);

        foreach (Transform child in LoadContent)
        {
            Destroy(child.gameObject);
        }

        foreach (string file in files)
        {
            LoadBox loadBox = Instantiate(LoadBoxPrefab, LoadContent);
            loadBox.Setup(file);
        }
    }

    private void SetActivePanel(Transform paneltoShow)
    {
        previousPanel = currentPanel;
        currentPanel = paneltoShow;

        MainPanel.gameObject.SetActive(paneltoShow == MainPanel);
        GuidePanel.gameObject.SetActive(paneltoShow == GuidePanel);
        PlayPanel.gameObject.SetActive(paneltoShow == PlayPanel);
        LoadPanel.gameObject.SetActive(paneltoShow == LoadPanel);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

[Serializable]
public enum ButtonAction
{
    Play,
    Guide,
    Exit,
    NewGame,
    LoadGame,
    Back
}
