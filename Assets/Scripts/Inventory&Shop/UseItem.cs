using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{

    private PlayerStatsUpgrade playerStatsUpgrade;

    private void Awake()
    {
        playerStatsUpgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsUpgrade>();
    }

    public void ApplyItemEffects(ItemSO itemSO)
    {
        AdjustStats(itemSO, 1);

        Debug.Log(itemSO);

        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        AdjustStats(itemSO, -1);
    }

    private void AdjustStats(ItemSO itemSO, int multiplier)
    {
        if (itemSO.currentHealth > 0)
            playerStatsUpgrade.UpdateHealth(itemSO.currentHealth * multiplier);
        if (itemSO.maxHealth > 0)
            playerStatsUpgrade.UpdateMaxHealth(itemSO.maxHealth * multiplier);
        if (itemSO.speed > 0)
            playerStatsUpgrade.UpdateSpeed(itemSO.speed * multiplier);

    }
}
