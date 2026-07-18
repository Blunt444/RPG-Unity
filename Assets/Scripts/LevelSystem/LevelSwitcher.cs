using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public static LevelSwitcher Instance;
    public LevelSystem levelSystem;

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

    public void SwitchLevelMode(PlayerStance playerStance)
    {
        levelSystem.GetAndSetValueInSystem(playerStance == PlayerStance.Warrior ? PlayerStance.Archer : PlayerStance.Warrior);
        levelSystem.SetValueInExpManager(playerStance);
    }

}
