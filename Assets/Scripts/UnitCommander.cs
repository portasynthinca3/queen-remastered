using UnityEngine;
using UnityEngine.Events;

public class UnitCommander : MonoBehaviour
{
    public ClickReceiver clickReceiver;

    private void Awake()
    {
        clickReceiver.onClick.AddListener(MoveUnits);
    }

    private void MoveUnits(Vector2 screenCoordinates)
    {
        Debug.Log("Move Units");
    }
}