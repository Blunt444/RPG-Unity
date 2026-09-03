using System;
using UnityEngine;

public class StanceManager : MonoBehaviour
{
    public static StanceManager Instance;


    private int warriorStancePoint = 1;
    private int archeryStancePoint = 5;
    private Player_Combat playerWarrior;
    private Player_Bow playerArcher;
    private GameObject bowObject;
    public PlayerStance playerStance = PlayerStance.Warrior;
    private bool isArcherStanceUnlocked = false;
    private bool isStanceChangerBlocked = false;
    public int messageTimer = 2;
    public static event Action<string, int> Message;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerWarrior = gameObject.GetComponent<Player_Combat>();
            playerArcher = gameObject.GetComponent<Player_Bow>();
            bowObject = transform.Find("Bow").gameObject;
            SetStanceAtLoad();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetStanceAtLoad()
    {
        playerWarrior.enabled = true;
        playerArcher.enabled = false;
        bowObject.SetActive(false);
    }

    public void ChangeStance()
    {
        if (isStanceChangerBlocked)
        {
            Message?.Invoke("Switching stance is blocked.", messageTimer);
            return;
        }
        else if (!isArcherStanceUnlocked)
        {
            Debug.Log("Archery");
            Message?.Invoke("Archery stance is yet to be unlocked.", messageTimer);
            return;
        }

        switch (playerStance)
        {
            case PlayerStance.Warrior:
                playerWarrior.enabled = false;
                playerArcher.enabled = true;
                bowObject.SetActive(true);
                playerStance = PlayerStance.Archer;
                LevelSwitcher.Instance.SwitchLevelMode(playerStance);
                ArrowQuantityManager.Instance.DisplayCanvas();
                break;
            case PlayerStance.Archer:
                playerWarrior.enabled = true;
                playerArcher.enabled = false;
                bowObject.SetActive(false);
                playerStance = PlayerStance.Warrior;
                LevelSwitcher.Instance.SwitchLevelMode(playerStance);
                ArrowQuantityManager.Instance.HideCanvas();
                break;
            default:
                break;
        }
    }

    public bool isSwitchingStanceAllowed()
    {
        if (isStanceChangerBlocked)
        {
            Message?.Invoke("Switching stance is blocked.", messageTimer);
            return false;
        }
        else if (!isArcherStanceUnlocked)
        {
            Debug.Log("Archery");
            Message?.Invoke("Archery stance is yet to be unlocked.", messageTimer);
            return false;
        }

        return true;
    }

    public void UnlockArcherStance()
    {
        isArcherStanceUnlocked = true;
    }

    public void BlockSwitchingStance()
    {
        isStanceChangerBlocked = true;
    }

    public void UnblockSwitchingStance()
    {
        isStanceChangerBlocked = false;
    }

    public void ChangePointToRespectiveStance(int amount)
    {
        if (playerStance == PlayerStance.Warrior)
        {
            warriorStancePoint += amount;
        }
        else if (playerStance == PlayerStance.Archer)
        {
            archeryStancePoint += amount;
        }
    }

    public int GetPointsForRespectiveStance(SkillCategory type)
    {
        if (type == SkillCategory.Combat)
        {
            return warriorStancePoint;
        }
        return archeryStancePoint;
    }

    public void ChangePointToRespectiveStance(SkillCategory type, int amount)
    {
        if (type == SkillCategory.Combat)
        {
            warriorStancePoint += amount;
        }
        else
        {
            archeryStancePoint += amount;
        }
    }

    public void SetPointsToStance(SkillCategory type, int amount)
    {
        if (type == SkillCategory.Combat) warriorStancePoint = amount;
        else archeryStancePoint = amount;
    }
}

public enum PlayerStance
{
    Warrior,
    Archer
}
