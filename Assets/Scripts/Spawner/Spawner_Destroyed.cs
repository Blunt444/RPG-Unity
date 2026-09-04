using UnityEngine;

public class Spawner_Destroyed : MonoBehaviour
{
    public Sprite destroyedSprite;

    private GameObject spawnerHutContainer;
    private GameObject healthCanvas;

    private void Awake()
    {
        spawnerHutContainer = transform.parent.gameObject;
        healthCanvas = transform.parent.Find("Health").gameObject;
    }

    private void Start()
    {
        if (gameObject.GetComponent<Spawner_Manager>().isDead)
        {
            OnDestroyed();
        }
    }

    public void OnDestroyed()
    {
        if (destroyedSprite == null)
        {
            Destroy(spawnerHutContainer);
            return;
        }
        GetComponent<SpriteRenderer>().sprite = destroyedSprite;
        Destroy(healthCanvas);
    }
}
