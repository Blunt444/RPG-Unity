using Unity.Mathematics;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public Vector2 aimDirection;

    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity);
    }
}
