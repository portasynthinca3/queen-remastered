using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    public Vector2Event onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(eventData.position);
    }
}