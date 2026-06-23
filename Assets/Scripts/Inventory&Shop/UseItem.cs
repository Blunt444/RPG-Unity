using System.Collections;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public void ApplyItemEffects(ItemSO itemSO)
    {
        AdjustStats(itemSO, 1);

        if (itemSO.duration > 0)
            StartCoroutine(EffectTimer(itemSO, itemSO.duration));
    }

    private IEnumerator EffectTimer(ItemSO itemSO, float duration)
    {
        yield return new WaitForSeconds(duration);
        AdjustStats(itemSO, -1);
    }

    private void AdjustStats(ItemSO itemSO, int multiplier)
    {
        if (itemSO.currentHealth > 0)
            StatsManager.Instance.UpdateHealth(itemSO.currentHealth * multiplier);
        if (itemSO.maxHealth > 0)
            StatsManager.Instance.UpdateMaxHealth(itemSO.maxHealth * multiplier);
        if (itemSO.speed > 0)
            StatsManager.Instance.UpdateSpeed(itemSO.speed * multiplier);
    }
}
