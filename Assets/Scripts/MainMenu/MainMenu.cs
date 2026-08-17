using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public MainMenuButton clickedButton;
    public List<MainMenuButton> allButtons = new List<MainMenuButton>();

    public string newScene = "Scene1";

    public Stack<Transform> stack = new Stack<Transform>();

    public Transform MainPanel;
    public Transform GuidePanel;
    public Transform PlayPanel;
    public Transform LoadPanel;
    public Transform LoadContent;
    public Transform LoadNoContent;
    public LoadBox LoadBoxPrefab;
    public Transform currentPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        stack.Clear();
        OpenPanel(MainPanel);
        StartCoroutine(SuppressEventSystem());
    }

    private IEnumerator SuppressEventSystem()
    {
        EventSystem es = EventSystem.current;
        if (es == null) yield break;
        es.enabled = false;
        yield return null;
        yield return null;
        yield return null;
        es.enabled = true;
    }

    public void ButtonClicked(MainMenuButton button)
    {
        clickedButton = button;

        switch (button.action)
        {
            case ButtonAction.Guide:
                OpenPanel(GuidePanel);
                return;
            case ButtonAction.Play:
                OpenPanel(PlayPanel);
                return;
            case ButtonAction.Exit:
                QuitGame();
                return;
            case ButtonAction.NewGame:
                SceneManager.LoadScene(newScene);
                return;
            case ButtonAction.LoadGame:
                ShowAllSaves();
                OpenPanel(LoadPanel);
                return;
            case ButtonAction.Back:
                Back();
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

    private void ShowPanel(Transform paneltoShow)
    {
        MainPanel.gameObject.SetActive(paneltoShow == MainPanel);
        GuidePanel.gameObject.SetActive(paneltoShow == GuidePanel);
        PlayPanel.gameObject.SetActive(paneltoShow == PlayPanel);
        LoadPanel.gameObject.SetActive(paneltoShow == LoadPanel);
    }

    private void OpenPanel(Transform panelToShow)
    {
        if (panelToShow == null || panelToShow == currentPanel) return;

        if (currentPanel != null)
            stack.Push(currentPanel);
        currentPanel = panelToShow;
        ShowPanel(currentPanel);
    }

    private void Back()
    {
        if (stack.Count == 0)
        {
            currentPanel = MainPanel;
            ShowPanel(currentPanel);
            return;
        }
        Transform panel = stack.Pop();
        currentPanel = panel;
        ShowPanel(currentPanel);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
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
