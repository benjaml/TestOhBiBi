using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeElement : MonoBehaviour
{
    [Serializable]
    public struct UpgradeStep
    {
        public int Cost;
        public int Value;
    }
    [SerializeField]
    private string _valueName;

    [SerializeField]
    private UpgradeStep[] _steps;

    [SerializeField]
    private Text _costText;
    [SerializeField]
    private Text _valueText;

    private int _currentStep;


    private void Update()
    {
        CheckCurrency();
    }

    private void OnEnable()
    {
        _currentStep = PlayerPrefs.GetInt(_valueName + "Step");
        CheckCurrency();
        UpdateTexts();
    }

    public void Upgrade()
    {
        PlayerWallet.Instance.AddSoftCurrency(-_steps[_currentStep].Cost);
        _currentStep++;
        PlayerPrefs.SetInt(_valueName + "Step", _currentStep);
        UpdateTexts();
        CheckCurrency();
    }

    private void CheckCurrency()
    {
        Button button = GetComponentInChildren<Button>();
        if(_currentStep == _steps.Length || PlayerWallet.Instance.GetSoftCurrency() < _steps[_currentStep].Cost)
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
        if (_currentStep < _steps.Length)
        {
            _valueText.text = _steps[_currentStep].Value.ToString();
            _costText.text = _steps[_currentStep].Cost.ToString();
        }
        else
        {
            _valueText.text = "";
            _costText.text = "";
        }
    }
}
