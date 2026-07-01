using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{

    public DialogSO currentConvo;
    public List<DialogSO> conversations;

    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Animator interactionAnim;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
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
            if (DialogueManager.Instance.isDialogueActive)
            {
                DialogueManager.Instance.AdvanceDialogue();
            }
            else
            {
                CheckForNewConversation();
                DialogueManager.Instance.StartDialogue(currentConvo );
            }
        }
    }

    private void CheckForNewConversation()
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            var convo = conversations[i];
            if(convo != null && convo.IsConditionMet())
            {
                conversations.RemoveAt(i);
                currentConvo = convo;
            }
        }
    }
}
