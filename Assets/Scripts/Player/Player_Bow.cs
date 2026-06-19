using Unity.Mathematics;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPointHorizontal;
    public Transform launchPointVertical;
    public Transform currentLaunchPoint;
    public GameObject arrowPrefab;
    public Vector2 aimDirection;
    public float shootCooldown;

    private float shootTimer;

    private void Start()
    {
        currentLaunchPoint = launchPointHorizontal;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, currentLaunchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Launch(aimDirection);
        shootTimer = shootCooldown;

        if (arrow == null)
        {
            Debug.LogError("Arrow component not found on prefab!");
            return;
        }
        if (currentLaunchPoint == null)
        {
            Debug.LogError("currentLaunchPoint is null!");
            return;
        }
    }
    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
            if (vertical > 0)
            {
                currentLaunchPoint = launchPointVertical;
                Vector3 pos = launchPointVertical.localPosition;
                launchPointVertical.localPosition = new Vector3(pos.x, Mathf.Abs(pos.y), pos.z);
            }
            else if (vertical < 0)
            {
                currentLaunchPoint = launchPointVertical;
                Vector3 pos = launchPointVertical.localPosition;
                launchPointVertical.localPosition = new Vector3(pos.x, -Mathf.Abs(pos.y), pos.z);
            }
            else
            {
                currentLaunchPoint = launchPointHorizontal;
            }

        }
    }
}
