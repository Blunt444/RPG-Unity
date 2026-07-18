using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int warriorLevel = 0;
    private int warriorCurrExp = 0;
    private int warriorExpToLevel = 10;
    private float warriorExpGrowth = 1.2f;
    private int archeryLevel = 0;
    private int archeryCurrExp = 0;
    private int archeryExpToLevel = 10;
    private float archeryExpGrowth = 1.2f;

    public void SetValueInExpManager(PlayerStance playerStance)
    {
        if (playerStance == PlayerStance.Warrior)
            ExpManager.Instance.SetValues(
                warriorLevel, warriorCurrExp, warriorExpToLevel, warriorExpGrowth
            );
        else if (playerStance == PlayerStance.Archer)
            ExpManager.Instance.SetValues(
                archeryLevel, archeryCurrExp, archeryExpToLevel, archeryExpGrowth
            );
    }

    public void GetAndSetValueInSystem(PlayerStance playerStance)
    {
        Dictionary<string, float> dict = ExpManager.Instance.GetValues();
        if (playerStance == PlayerStance.Warrior)
        {
            warriorLevel = (int)dict["level"];
            warriorCurrExp = (int)dict["currentExp"];
            warriorExpToLevel = (int)dict["expToLevel"];
            warriorExpGrowth = dict["expGrowthMultiplier"];

        }
        else if (playerStance == PlayerStance.Archer)
        {
            archeryLevel = (int)dict["level"];
            archeryCurrExp = (int)dict["currentExp"];
            archeryExpToLevel = (int)dict["expToLevel"];
            archeryExpGrowth = dict["expGrowthMultiplier"];
        }
    }
}
