using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistence Objects")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
        else
        {
            CleanUpAndDestroy();
            return;
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj == null) continue;
            DontDestroyOnLoad(obj);
        }
    }
    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null) Destroy(obj);
        }
        Destroy(gameObject);
    }

    public void CleanUpPersistentObject()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null) Destroy(obj);
        }
        Destroy(gameObject);
        Instance = null;
    }
}
