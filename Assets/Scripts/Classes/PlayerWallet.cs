using UnityEngine;

public class PlayerWallet
{
    // As I started to handle currency a bit everywhere in the code, so I choose to 
    // create this class to handle it. This allows to have the name of PlayerPrefs only
    // defined here which would be easier to change if needed.
    // Also this class have a Static instance and as this is not a MonoBehaviour, this 
    // can be used everywhere.

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
                return int.MaxValue;
            default:
                return 0;
        }
    }

}
