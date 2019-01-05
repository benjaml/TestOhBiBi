using UnityEngine;

public class HealPickup : MonoBehaviour
{
    [SerializeField]
    private float _percentage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<EntityHealth>().Heal(_percentage);
            Destroy(gameObject);
        }
    }
}
