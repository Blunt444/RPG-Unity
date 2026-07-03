using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    public DialogSO dialogSO;
    int currentIndex;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Animator interactionAnim;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        currentIndex = dialogSO.returnStartIndex;
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        anim.SetBool("isWalking", false);
        interactionAnim.Play("OpenIcon");
    }

    private void OnDisable()
    {
        interactionAnim.Play("CloseIcon");
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("Line No:" + currentIndex);
            if(currentIndex != -1 && DialogueManager.Instance.isOpened)
            {
                Debug.Log("Advance");
            }
            else
            {
                DialogueManager.Instance.ToggleVisibility();
            }
        }
    }


}
