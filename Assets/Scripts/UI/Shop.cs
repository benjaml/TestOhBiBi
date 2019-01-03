using UnityEngine;

public class Shop : MonoBehaviour
{
    
    public void BuyCurrency(int amount)
    {
        // Demo buy
        PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + amount);
    }
}
