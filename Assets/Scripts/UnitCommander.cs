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

        Vector3 particlePosition = Camera.main.ScreenToWorldPoint(screenCoordinates);
        particlePosition.z = 0f;
        ParticleManager.Instance.CreateMoveUnitsParticle(particlePosition);
    }
}