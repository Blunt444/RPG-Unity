using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public string respawnPointId;
    private bool isPlayerInRange = false;


    public string GetRespawnPointId()
    {
        return respawnPointId;
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetButtonDown("Enter"))
            {
                SetAsCheckPoint();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void SetAsCheckPoint()
    {
        RespawnPointManager.Instance.SetRespawnPoint(this);
    }
}
