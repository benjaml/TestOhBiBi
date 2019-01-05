using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField]
    private int _coinPerWave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.CollectSoftCurrency(_coinPerWave);
            Destroy(gameObject);
        }
    }
}
