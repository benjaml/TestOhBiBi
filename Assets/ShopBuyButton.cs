using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButton : MonoBehaviour
{
    [SerializeField]
    private PlayerWallet.CurrencyType _costCurrencyType;
    [SerializeField]
    private int _costAmount;
    [SerializeField]
    private PlayerWallet.CurrencyType _gainCurrencyType;
    [SerializeField]
    private int _gainAmount;

    private Button _button;
    [SerializeField]
    private Text _costText;
    [SerializeField]
    private Text _gainText;


    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(BuyCurrency);
        if(_costCurrencyType == PlayerWallet.CurrencyType.Real)
        {
            _costText.text = (_costAmount*0.01f).ToString("F2") + " $";
        }
        else
        {
            _costText.text = _costAmount.ToString();
        }
        if (_gainCurrencyType == PlayerWallet.CurrencyType.Real)
        {
            _gainText.text = (_gainAmount * 0.01f).ToString("F2") + " $";
        }
        else
        {
            _gainText.text = _gainAmount.ToString();
        }
    }

    void BuyCurrency()
    {
        PlayerWallet.Instance.AddCurrency(_costCurrencyType, -_costAmount);
        PlayerWallet.Instance.AddCurrency(_gainCurrencyType, _gainAmount);
    }

    private void Update()
    {
        CheckCurrency();
    }

    private void CheckCurrency()
    {
        if (PlayerWallet.Instance.GetCurrency(_costCurrencyType) < _costAmount)
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }
}
