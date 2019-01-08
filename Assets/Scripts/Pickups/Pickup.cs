using UnityEngine;

public class Pickup : MonoBehaviour
{
    // This delegate is used to free the spawner from where the pickup spawned
    public delegate void FreeSpawnSpot();
    protected FreeSpawnSpot _freeSpawnCallback;

    public void SetPickupCallback(FreeSpawnSpot callback)
    {
        _freeSpawnCallback = callback;
    }
}
