using UnityEngine;

public class ElevationEntry : MonoBehaviour
{
    public Collider2D[] mountainColliders;
    public Collider2D[] mountainBoundarys;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            foreach (Collider2D mountain in mountainColliders)
            {
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in mountainBoundarys)
            {
                boundary.enabled = true;
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {

    //         foreach (Collider2D mountain in mountainColliders)
    //         {
    //             mountain.enabled = true;
    //         }
    //         foreach (Collider2D boundary in mountainBoundarys)
    //         {
    //             boundary.enabled = false;
    //         }
    //         collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 5;

    //     }
    // }
}
