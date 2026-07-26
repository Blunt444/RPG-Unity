using UnityEngine;

public class Enemy_Set_Uncontrolled_Chase_False : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += SetUncontrolledChaseFalseOnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= SetUncontrolledChaseFalseOnPlayerDeath;
    }

    public void SetUncontrolledChaseFalseOnPlayerDeath()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Torch");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy_Movement>().ResetChaseUncontrolled();
        }
    }
}
