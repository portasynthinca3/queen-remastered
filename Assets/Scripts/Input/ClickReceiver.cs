using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour, IPointerClickHandler
{
    public Vector2Event onLeftClick;
    public Vector2Event onRightClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        onLeftClick?.Invoke(eventData.position);
        else if(eventData.button == PointerEventData.InputButton.Right)
        onRightClick?.Invoke(eventData.position);
    }
}