using UnityEngine;
using UnityEngine.SceneManagement;

public class Id : MonoBehaviour
{
    public static string CreateId(Vector3 pos)
    {
        string sceneName = SceneManager.GetActiveScene().name; 
        return $"{sceneName}_{Mathf.RoundToInt(pos.x * 1000)}_{Mathf.RoundToInt(pos.y * 1000)}";
    }
}
