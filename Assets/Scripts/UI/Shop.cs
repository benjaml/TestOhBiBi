using UnityEngine;

public class Shop : MonoBehaviour
{
    
    public void BuyCurrency(int amount)
    {
        // Demo buy
        PlayerWallet.Instance.AddHardCurrency(amount);
    }
}
