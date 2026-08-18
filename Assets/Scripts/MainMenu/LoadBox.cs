using TMPro;
using UnityEngine;

public class LoadBox : MonoBehaviour
{
    public string fileName;
    public TMP_Text text;
    public LoadButton deleteButton;
    public LoadButton loadButton;

    public void Setup(string fileName)
    {
        this.fileName = fileName;
        text.text = fileName;
        deleteButton.fileName = fileName;
        loadButton.fileName = fileName;
    }

    private void OnEnable()
    {
        SaveManager.buttonResponse += ButtonResponseAction;
    }

    private void OnDisable()
    {
        SaveManager.buttonResponse -= ButtonResponseAction;
    }

    private void ButtonResponseAction(string fileName, LoadButtonAction action, bool state)
    {
        if (!state || fileName != this.fileName) return;

        switch (action)
        {
            case LoadButtonAction.Load:
                SaveManager.Instance.LoadGame(fileName);
                return;
            case LoadButtonAction.Delete:
                DeleteChild(fileName);
                return;
            default:
                return;
        }
    }

    private void DeleteChild(string fileName)
    {
        if(this.fileName == fileName)
        {
            Destroy(gameObject);
        }
        else
        {
            
        }
    }
}
