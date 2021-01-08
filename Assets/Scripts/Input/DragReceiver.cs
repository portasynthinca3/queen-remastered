using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DragReceiver : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Vector2Event onDrag;
    public UnityEvent onBeginDrag;
    public UnityEvent onEndDrag;

    public void OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData.delta);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke();
    }    
    
    public void OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke();
    }
}