using UnityEngine;

public class StarPickup : MonoBehaviour
{
    [SerializeField]
    private float _starPerWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.CollectHardCurrency(_starPerWave);
            Destroy(gameObject);
        }
    }
}
