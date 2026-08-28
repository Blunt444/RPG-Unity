// using TMPro;
// using UnityEngine;
// using UnityEngine.EventSystems;

// public class DialogChoiceText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
// {
//     public TMP_Text label;
//     private string text;

//     private void Awake()
//     {
//         if (label == null)
//         {
//             label = GetComponent<TMP_Text>();
//         }
//     }

//     public void UpdateText(string text)
//     {
//         label.text = text;
//         this.text = text;
//     }

//     public void OnPointerEnter(PointerEventData eventData)
//     {
//         label.text = "<u>" + text + "</u>";
//     }

//     public void OnPointerExit(PointerEventData eventData)
//     {
//         label.text = text;
//     }

//     public void OnPointerDown(PointerEventData eventData)
//     {
        
//     }
// }

// Not using this script so far.