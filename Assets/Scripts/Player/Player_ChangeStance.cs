using UnityEngine;

public class Player_ChangeStance : MonoBehaviour
{
    public Player_Combat playerCombat;
    public Player_Bow playerBow;

    void Update()
    {
        if (Input.GetButtonDown("ToggleStance"))
        {
            playerCombat.enabled = !playerCombat.enabled;
            playerBow.enabled = !playerBow.enabled;
        }
    }
}
