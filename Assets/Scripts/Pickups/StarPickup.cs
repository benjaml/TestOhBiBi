using UnityEngine;

public class StarPickup : Pickup
{
    [SerializeField]
    private float _starPerWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // I do not use PlayerWallet as the GameManager handle store the currency of the game
            // to then add it to the PlayerWallet at the end of the game
            GameManager.Instance.CollectHardCurrency(_starPerWave);
            _freeSpawnCallback();
            Destroy(gameObject);
        }
    }
}
