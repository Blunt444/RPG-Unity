using UnityEngine;

public class Spawner_Destroyed : MonoBehaviour
{
    public Sprite destroyedSprite;

    private GameObject spawnerHutContainer;
    private GameObject healthCanvas;

    private void Awake()
    {
        spawnerHutContainer = transform.root.gameObject;
        healthCanvas = transform.root.Find("Health").gameObject;
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
