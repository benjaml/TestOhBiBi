using UnityEngine;

public class Shop : MonoBehaviour
{
    public void BuySoftCurrency(int amount)
    {
        // Demo buy
        PlayerWallet.Instance.AddSoftCurrency(amount);
    }

    public void BuyHardCurrency(int amount)
    {
        // Demo buy
        PlayerWallet.Instance.AddHardCurrency(amount);
    }
}
