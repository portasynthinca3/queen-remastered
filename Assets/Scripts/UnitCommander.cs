using UnityEngine;
using UnityEngine.Events;

public class UnitCommander : MonoBehaviour
{
    public ClickReceiver clickReceiver;

    private void Awake()
    {
        clickReceiver.onRightClick.AddListener(MoveUnits);
    }

    private void MoveUnits(Vector2 screenCoordinates)
    {
        if(!EntitySelector.Instance.UnitsSelected()) return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenCoordinates);
        ParticleManager.Instance.CreateMoveUnitsParticle(new Vector3(worldPosition.x, worldPosition.y, 0f));

        foreach(var entity in EntitySelector.Instance.GetSelectedEntities())
        {
            var unit = (Unit)entity;
            
            unit.MoveTo(worldPosition);
        }
    }
}