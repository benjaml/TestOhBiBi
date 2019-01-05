using UnityEngine;

public class PlayerWallet
{
    private static PlayerWallet _instance = null;
    public static PlayerWallet Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerWallet();
            }
            return _instance;
        }
    }

    public void AddHardCurrency(int amount)
    {
        PlayerPrefs.SetInt("HardCurrency", GetHardCurrency() + amount);
    }

    public void AddSoftCurrency(int amount)
    {
        PlayerPrefs.SetInt("SoftCurrency", GetSoftCurrency() + amount);
    }

    public int GetHardCurrency()
    {
        return PlayerPrefs.GetInt("HardCurrency");
    }

    public int GetSoftCurrency()
    {
        return PlayerPrefs.GetInt("SoftCurrency");
    }

}
