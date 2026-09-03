using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ClickDebugger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var pointerData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count == 0)
            {
                // Debug.Log("NOTHING was hit by raycast at click position.");
            }

            foreach (var result in results)
            {
                // Debug.Log("Hit: " + result.gameObject.name + " | Depth: " + result.depth + " | SortingLayer: " + result.sortingLayer);
            }
        }
    }
}