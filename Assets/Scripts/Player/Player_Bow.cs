using System;
using Unity.Mathematics;
using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public Vector2 aimDirection;
    public float shootCooldown;
    public Animator anim;
    public Animator bowAnim;
    public PlayerMovement playerMovement;
    public Camera mainCamera;

    private float shootTimer;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            bowAnim.SetBool("isShooting", true);
            playerMovement.ChangeState(PlayerState.Shooting);
        }
    }

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }
    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1);
        anim.SetLayerWeight(1, 0);
    }
    public void Shoot()
    {
        if (shootTimer > 0) return;

        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity).GetComponent<Arrow>();

        arrow.Launch(aimDirection);
        shootTimer = shootCooldown;

        playerMovement.ChangeState(PlayerState.Idle);
        playerMovement.isShooting = false;
        bowAnim.SetBool("isShooting", false);
    }
    public void HandleAiming()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = mainCamera.WorldToScreenPoint(transform.position).z;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        aimDirection = (mouseWorldPos - transform.position);
        aimDirection.Normalize();

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(aimDirection.x) > 0.05f)
        {
            if (aimDirection.x < 0)
            {
                transform.parent.localScale = new Vector3(
                 -Math.Abs(transform.parent.localScale.x),
                 transform.parent.localScale.y,
                 transform.parent.localScale.z
                );
                transform.localRotation = Quaternion.Euler(0, 0, 180f - angle);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.parent.localScale = new Vector3(
                 Math.Abs(transform.parent.localScale.x),
                 transform.parent.localScale.y,
                 transform.parent.localScale.z
                );
                transform.localRotation = Quaternion.Euler(0, 0, angle);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            if (aimDirection.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 180f - angle);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, angle);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}