using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    { 
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject obj = GameObject.FindWithTag("Confiner");
        if(obj == null) return;

        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        if(confiner == null) return;
        confiner.BoundingShape2D = obj.GetComponent<PolygonCollider2D>();
    }
}

