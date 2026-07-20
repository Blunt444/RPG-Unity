using UnityEngine;

public class StanceManager : MonoBehaviour
{
    public static StanceManager Instance;


    private int warriorStancePoint = 0;
    private int archeryStancePoint = 0;
    private Player_Combat playerWarrior;
    private Player_Bow playerArcher;
    private GameObject bowObject;
    private PlayerStance playerStance = PlayerStance.Warrior;
    private bool isArcherStanceUnlocked = true;
    private bool isStanceChangerBlocked = false;

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
        if (isStanceChangerBlocked) return;
        else if (!isArcherStanceUnlocked) return;

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
        if (isStanceChangerBlocked) return false;
        else if (!isArcherStanceUnlocked) return false;

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

    public int GetPointsForRespectiveStance()
    {
        if (playerStance == PlayerStance.Warrior)
        {
            return warriorStancePoint;
        }
        return archeryStancePoint;
    }
}

public enum PlayerStance
{
    Warrior,
    Archer
}
