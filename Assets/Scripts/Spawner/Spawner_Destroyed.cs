using UnityEngine;

public class Spawner_Destroyed : MonoBehaviour
{
    public Sprite destroyedSprite;
    public void OnDestroyed()
    {
        if (destroyedSprite == null)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<SpriteRenderer>().sprite = destroyedSprite;
    }
}
