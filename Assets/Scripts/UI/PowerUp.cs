using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private string _powerUpName;
    [SerializeField]
    private int _powerUpDuration;
    [SerializeField]
    private int _powerUpCost;

    [SerializeField]
    private Text _timerText;
    [SerializeField]
    private Text _costText;
    private Button _button;

    public void StartPowerUp()
    {
        PlayerPrefs.SetInt(_powerUpName, 1);
        PlayerPrefs.SetString(_powerUpName + "Start", DateTime.Now.ToLongTimeString());
        PlayerPrefs.SetInt(_powerUpName + "Duration", _powerUpDuration);
        PlayerWallet.Instance.AddCurrency(PlayerWallet.CurrencyType.Hard, -_powerUpCost);
    }

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
        _costText.text = _powerUpCost.ToString();
    }

    private void Update()
    {
        bool powerUpActivated = PlayerPrefs.GetInt(_powerUpName) == 1;
        _button.interactable = !powerUpActivated && PlayerWallet.Instance.GetCurrency(PlayerWallet.CurrencyType.Hard) > _powerUpCost;
        if(powerUpActivated)
        {
            TimeSpan elapsedTime = (DateTime.Now - DateTime.Parse(PlayerPrefs.GetString(_powerUpName + "Start")));
            TimeSpan remainingTime = TimeSpan.FromMinutes(_powerUpDuration) - elapsedTime;
            if(remainingTime <= new TimeSpan())
            {
                PlayerPrefs.SetInt(_powerUpName, 0);
            }
            else
            {
                _timerText.text = remainingTime.ToString().Split('.')[0];
            }
        }
        else
        {
            _timerText.text = "";
        }
    }
}
