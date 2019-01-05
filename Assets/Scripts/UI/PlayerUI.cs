using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance = null;
    
    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }
        else if(this != Instance)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private Text _waveInfoText;
    [SerializeField]
    private Text _currencyInfoText;
    [SerializeField]
    private Text _healthInfoText;
    [SerializeField]
    private Text _ammoInfoText;

    public void NotifyCurrency(int currentCurrency)
    {
        _currencyInfoText.text = currentCurrency.ToString();
    }

    public void NotifyAmmoCount(int currentCount, int max)
    {
        _ammoInfoText.text = $"{currentCount.ToString()} / {max.ToString()}";
    }

    public void NotifyHealth(int health)
    {
        _healthInfoText.text = health.ToString();
    }

    public void ChangeWaveInfoMessage(string message)
    {
        _waveInfoText.text = message;
    }
}
