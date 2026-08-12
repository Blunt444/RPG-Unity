using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DeathCanvasScript : MonoBehaviour
{
    public static DeathCanvasScript Instance;
    public Transform playerTransform;
    public CanvasGroup canvas;
    public TMP_Text respawnTimer;
    public int Countdown = 5;


    private bool isOpen = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Respawn()
    {
        PlayerHealth playerHealth = playerTransform.gameObject.GetComponent<PlayerHealth>();

        playerTransform.gameObject.GetComponent<PlayerStatsUpgrade>().UpdateHealth(StatsManager.Instance.maxHealth);

        playerTransform.position = playerHealth.respawnPosition;
        playerTransform.gameObject.SetActive(true);

        playerTransform.GetComponent<PlayerMovement>().ResetMovements();

        ToggleVisibility();
    }

    public void OnDie()
    {
        ToggleVisibility();
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        float elapsed = Countdown;
        while (elapsed > 0)
        {
            respawnTimer.text = "Respawing in " + Mathf.CeilToInt(elapsed);

            elapsed -= Time.deltaTime;
            yield return null;
        }

        respawnTimer.text = "Respawning in 0";
        Respawn();
    }

    private void ToggleVisibility()
    {
        if (isOpen)
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
            isOpen = false;
        }
        else
        {
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
            isOpen = true;
        }
    }
}
