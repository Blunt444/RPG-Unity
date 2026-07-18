using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int warriorLevel = 0;
    private int warriorCurrExp = 0;
    private float warriorExpGrowth = 0;
    private int archeryLevel = 0;
    private int archeryCurrExp = 0;
    private float archeryExpGrowth = 0;

    public void SetValueInExpManager(PlayerStance playerStance)
    {
        if (playerStance == PlayerStance.Warrior)
            ExpManager.Instance.SetValues(
                warriorLevel, warriorCurrExp, warriorExpGrowth
            );
        else if (playerStance == PlayerStance.Archer)
            ExpManager.Instance.SetValues(
                archeryLevel, archeryCurrExp, archeryExpGrowth
            );
    }

    public void GetAndSetValueInSystem(PlayerStance playerStance)
    {
        Dictionary<string, float> dict = ExpManager.Instance.GetValues();
        if (playerStance == PlayerStance.Warrior)
        {
            warriorLevel = (int)dict["level"];
            warriorCurrExp = (int)dict["currentExp"];
            warriorExpGrowth = dict["expGrowthMultiplier"];

        }
        else if (playerStance == PlayerStance.Archer)
        {
            archeryLevel = (int)dict["level"];
            archeryCurrExp = (int)dict["currentExp"];
            archeryExpGrowth = dict["expGrowthMultiplier"];
        }
    }
}
