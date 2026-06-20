using UnityEngine;

public class Player_Bow : MonoBehaviour
{
    public Transform launchPointHorizontal;
    public Transform launchPointVertical;
    public Transform currentLaunchPoint;
    public GameObject arrowPrefab;
    public Vector2 aimDirection;
    public float shootCooldown;
    public Animator anim;
    public PlayerMovement playerMovement;

    private float shootTimer;

    private void Start()
    {
        currentLaunchPoint = launchPointHorizontal;
        anim.SetFloat("aimX", aimDirection.x);
        anim.SetFloat("aimY", aimDirection.y);
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetButtonDown("Shoot") && shootTimer <= 0)
        {
            playerMovement.isShooting = true;
            gameObject.GetComponent<PlayerMovement>().ChangeState(PlayerState.Shooting);
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
        Arrow arrow = Instantiate(arrowPrefab, currentLaunchPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Launch(aimDirection);
        shootTimer = shootCooldown;
        gameObject.GetComponent<PlayerMovement>().ChangeState(PlayerState.Idle);
        playerMovement.isShooting = false;

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
            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
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
