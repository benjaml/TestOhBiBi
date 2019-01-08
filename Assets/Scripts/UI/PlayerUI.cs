using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    // I did a singleton for this class as I'm using it to update the player UI from other scripts
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
    private Text _softCurrencyInfoText;
    [SerializeField]
    private Text _hardCurrencyInfoText;
    [SerializeField]
    private Image _healthImage;
    [SerializeField]
    private Text _ammoInfoText;

    public void NotifySoftCurrency(int currentCurrency)
    {
        _softCurrencyInfoText.text = currentCurrency.ToString();
    }

    public void NotifyHardCurrency(int currentCurrency)
    {
        _hardCurrencyInfoText.text = currentCurrency.ToString();
    }

    public void NotifyAmmoCount(int currentCount, int max)
    {
        _ammoInfoText.text = $"{currentCount.ToString()} / {max.ToString()}";
    }

    public void NotifyHealth(float ratio)
    {
        _healthImage.fillAmount = ratio;
    }

    public void ChangeWaveInfoMessage(string message)
    {
        _waveInfoText.text = message;
    }
}
