using UnityEngine;

public class HealPickup : Pickup
{
    [SerializeField]
    private float _percentage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<EntityHealth>().Heal(_percentage);
            _freeSpawnCallback();
            Destroy(gameObject);
        }
    }
}
