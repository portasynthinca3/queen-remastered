using UnityEngine;
using UnityEngine.EventSystems;

public class DragReceiver : MonoBehaviour, IDragHandler
{
    public Vector2Event onDrag;

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData.position);
    }
}