using UnityEngine;

public class StarPickup : Pickup
{
    [SerializeField]
    private float _starPerWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.CollectHardCurrency(_starPerWave);
            _freeSpawnCallback();
            Destroy(gameObject);
        }
    }
}
