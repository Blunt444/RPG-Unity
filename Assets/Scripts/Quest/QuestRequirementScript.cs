using TMPro;
using UnityEngine;

public class QuestRequirementScript : MonoBehaviour
{
    public TMP_Text label;
    public TMP_Text progress;

    public void Setup(string label, string progress, bool completed)
    {
        this.label.text = completed ? $"<s>{label}</s>" : label;
        this.progress.text = completed ? $"<s>{progress}</s>" : progress;

        if (completed)
        {
            this.label.alpha = 0.5f;
            this.progress.alpha = 0.5f;
        }
    }

    public void Setup(string label, string progress)
    {
        this.label.text = label;
        this.progress.text = progress;
    }

}
