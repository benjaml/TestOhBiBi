using UnityEngine;
using UnityEngine.UI;

public class UpgradeElement : MonoBehaviour
{
    public enum AugmentationMethod
    {
        Add,
        Multiply
    }

    [SerializeField]
    private string _valueName;
    [SerializeField]
    private AugmentationMethod _augmentationMethod;
    [SerializeField]
    private int _startCost;
    [SerializeField]
    private int _augmentationAmount;
    [SerializeField]
    private Text _costText;
    [SerializeField]
    private Text _valueText;

    private int _currentCost;
    private int _currentStep;


    private void Update()
    {
        CheckCurrency();
    }

    private void OnEnable()
    {
        _currentStep = PlayerPrefs.GetInt(_valueName);
        _currentCost = CalculateCost();
        CheckCurrency();
        UpdateTexts();
    }

    private int CalculateCost()
    {
        switch (_augmentationMethod)
        {
            case AugmentationMethod.Add:
                return _startCost + _currentStep * _augmentationAmount;
            case AugmentationMethod.Multiply:
                return (int)(_startCost * Mathf.Pow(_augmentationAmount, _currentStep));
        }
        return 0;
    }

    public void Upgrade()
    {
        PlayerWallet.Instance.AddCurrency(PlayerWallet.CurrencyType.Soft, -_currentCost);
        _currentStep++;
        _currentCost = CalculateCost();
        PlayerPrefs.SetInt(_valueName, _currentStep);
        UpdateTexts();
        CheckCurrency();
    }

    private void CheckCurrency()
    {
        Button button = GetComponentInChildren<Button>();
        if( PlayerWallet.Instance.GetCurrency(PlayerWallet.CurrencyType.Soft) < _currentCost)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    private void UpdateTexts()
    {
        _valueText.text = PlayerPrefs.GetInt(_valueName).ToString();
        _costText.text = _currentCost.ToString();
    }
}