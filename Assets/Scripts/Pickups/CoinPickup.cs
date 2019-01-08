using UnityEngine;

public class CoinPickup : Pickup
{
    [SerializeField]
    private int _coinPerWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // I do not use PlayerWallet as the GameManager handle store the currency of the game
            // to then add it to the PlayerWallet at the end of the game
            GameManager.Instance.CollectSoftCurrency(_coinPerWave);
            _freeSpawnCallback();
            Destroy(gameObject);
        }
    }
}
