using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance { get; private set; }

    public GameObject moveUnitsHereParticlePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateMoveUnitsParticle(Vector3 worldPosition)
    {
        Instantiate(moveUnitsHereParticlePrefab, worldPosition, Quaternion.identity);
    }
}