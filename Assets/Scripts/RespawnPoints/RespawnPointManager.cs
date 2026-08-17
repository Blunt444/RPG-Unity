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

    [SerializeField] private List<RespawnPoint> respawnPoints = new List<RespawnPoint>();

    public string GetCurrentRespawnPointId()
    {
        return respawnPoint.GetRespawnPointId();
    }

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
        if (respawnPoint == this.respawnPoint) return;
        this.respawnPoint = respawnPoint;
        TriggerAcknowledgement();
    }

    public void SetRespawnPoint(string id)
    {
        foreach(RespawnPoint respawnPoint in respawnPoints)
        {
            if(respawnPoint.respawnPointId == id)
            {
                this.respawnPoint = respawnPoint;
                return;
            }
        }

        respawnPoint = defaultRespawnPoint;
    }

    private void TriggerAcknowledgement()
    {
        StartCoroutine(DisableMessage());
    }

    private IEnumerator DisableMessage()
    {
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(countdown);
        message.gameObject.SetActive(false);
    }
}
