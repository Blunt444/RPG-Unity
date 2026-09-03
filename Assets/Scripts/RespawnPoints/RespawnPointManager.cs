using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespawnPointManager : MonoBehaviour
{
    public static RespawnPointManager Instance;
    public RespawnPoint respawnPoint;
    public TMP_Text message;
    public int countdown = 3;
    public RespawnPoint defaultRespawnPoint;
    public List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    public string GetCurrentRespawnPointId()
    {
        if (respawnPoint == null)
        {
            return defaultRespawnPoint != null ? defaultRespawnPoint.GetRespawnPointId() : null;
        }

        return respawnPoint.GetRespawnPointId();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            respawnPoints.Clear();
            respawnPoint = defaultRespawnPoint;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetRespawnPoint(RespawnPoint respawnPoint)
    {
        if (respawnPoint == this.respawnPoint)
        {
            message.text = "Respawn Point Already Set!";
            TriggerAcknowledgement();
            return;
        }
        this.respawnPoint = respawnPoint;
        message.text = "Respawn Point Set!";
        TriggerAcknowledgement();
    }

    public void SetRespawnPoint(string id)
    {
        foreach (RespawnPoint respawnPoint in respawnPoints)
        {
            if (respawnPoint.respawnPointId == id)
            {
                this.respawnPoint = respawnPoint;
                return;
            }
            Debug.Log(id);
            Debug.Log(respawnPoint.respawnPointId);

        }
        Debug.Log(defaultRespawnPoint.respawnPointId);
        respawnPoint = defaultRespawnPoint;
    }

    private void TriggerAcknowledgement()
    {
        StartCoroutine(DisableMessage());
    }

    private IEnumerator DisableMessage()
    {
        message.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(countdown);
        message.gameObject.SetActive(false);
    }
}
