using UnityEngine;

public class Pickup : MonoBehaviour
{
    public delegate void FreeSpawnSpot();
    protected FreeSpawnSpot _freeSpawnCallback;

    public void SetPickupCallback(FreeSpawnSpot callback)
    {
        _freeSpawnCallback = callback;
    }

}
