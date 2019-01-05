using UnityEngine;

public class CoinPickup : Pickup
{
    [SerializeField]
    private int _coinPerWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.CollectSoftCurrency(_coinPerWave);
            _freeSpawnCallback();
            Destroy(gameObject);
        }
    }
}
