using UnityEngine;

public class PlayerWallet
{
    public enum CurrencyType
    {
        Soft,
        Hard,
        Real
    }

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

    public void AddCurrency(CurrencyType type, int amount)
    {
        int newAmount = GetCurrency(type) + amount;
        if (type == CurrencyType.Hard)
        {
            PlayerPrefs.SetInt("HardCurrency", newAmount);
        }
        else if(type == CurrencyType.Soft)
        {
            PlayerPrefs.SetInt("SoftCurrency", newAmount);
        }
    }

    public int GetCurrency(CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.Soft:
                return PlayerPrefs.GetInt("SoftCurrency");
            case CurrencyType.Hard:
                return PlayerPrefs.GetInt("HardCurrency");
            case CurrencyType.Real:
                return int.MaxValue; // If only life was this easy
            default:
                return 0;
        }
    }

}
