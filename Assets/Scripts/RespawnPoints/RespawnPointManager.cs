using System.Collections.Generic;
using UnityEngine;

public class RespawnPointManager : MonoBehaviour
{
    public static RespawnPointManager Instance;
    public RespawnPoint respawnPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetRespawnPoint(RespawnPoint respawnPoint)
    {
        this.respawnPoint = respawnPoint;
    }
}
