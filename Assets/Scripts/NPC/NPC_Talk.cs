using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;
public class NPC_Talk : MonoBehaviour
{
    public DialogSO dialogSO;
    public int currentIndex;
    public ActorSO actorSO;
    public QuestSO questSO;

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
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (anim == null) anim = GetComponent<Animator>();

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
            // Debug.Log("Line No:" + currentIndex);

            if (DialogueManager.Instance == null || dialogSO == null) return;

            if (currentIndex != -1 && DialogueManager.Instance.isOpened)
            {
                // Debug.Log("Advance");

                if (DialogHistoryTracker.Instance.CanShowNextLine(dialogSO.lines[currentIndex]) && (questSO == null || questSO.questState == QuestState.Completed))
                    currentIndex = DialogueManager.Instance.nextLineIndex(dialogSO, currentIndex);
                else
                    currentIndex = DialogueManager.Instance.EndConversation(dialogSO);


                if (currentIndex == -1)
                {
                    currentIndex = DialogueManager.Instance.EndConversation(dialogSO);
                }
                else
                {
                    DialogueManager.Instance.DisplayDialogue(dialogSO, currentIndex);
                }
            }
            else if (!DialogueManager.Instance.isOpened)
            {
                currentIndex = DialogueManager.Instance.GetStartIndex(dialogSO);
                DialogueManager.Instance.npc = this;

                if (dialogSO.lines[currentIndex].requiredActors.Count > 0 && DialogHistoryTracker.Instance.CanShowNextLine(dialogSO.lines[currentIndex]))
                    currentIndex = DialogueManager.Instance.nextLineIndex(dialogSO, currentIndex);

                if (questSO != null && questSO.questState == QuestState.Completed)
                {
                    currentIndex = DialogueManager.Instance.nextLineIndex(dialogSO, currentIndex);

                    if (questSO.label == "Clear the road up ahead.")
                    {
                        GameObject.FindGameObjectWithTag("Quest2AfterStartLock").GetComponent<TilemapCollider2D>().enabled = false;
                        GameObject.FindGameObjectWithTag("Quest2AfterStartLock").GetComponent<NavMeshModifier>().enabled = false;
                        Physics2D.SyncTransforms();
                        GameObject.FindFirstObjectByType<NavMeshSurface>().BuildNavMesh();
                    }
                    questSO = null;
                }

                DialogueManager.Instance.DisplayDialogue(dialogSO, currentIndex);

                DialogueManager.Instance.ToggleVisibility();
                DialogHistoryTracker.Instance.AddToTalkedNpc(actorSO);
            }
        }
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("isWalking", false);
    }

    public void SetLineIndex(int index)
    {
        currentIndex = index;
    }
}
