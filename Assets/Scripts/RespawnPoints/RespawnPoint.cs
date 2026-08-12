using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetAsCheckPoint();
        }
    }

    public void SetAsCheckPoint()
    {
        RespawnPointManager.Instance.SetRespawnPoint(this);
    }
}
