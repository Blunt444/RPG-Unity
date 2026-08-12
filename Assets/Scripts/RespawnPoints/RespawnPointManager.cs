using System.Collections;
using TMPro;
using UnityEngine;

public class RespawnPointManager : MonoBehaviour
{
    public static RespawnPointManager Instance;
    public RespawnPoint respawnPoint;
    public TMP_Text message;
    public int countdown = 3;

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
