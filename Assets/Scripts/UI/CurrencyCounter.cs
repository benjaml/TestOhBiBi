using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{

    private Text _text;
    [SerializeField]
    private bool _hardCurrency;

    void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    void Update()
    {
        if(_hardCurrency)
        {
            _text.text = PlayerWallet.Instance.GetCurrency(PlayerWallet.CurrencyType.Hard).ToString();
        }
        else
        {
            _text.text = PlayerWallet.Instance.GetCurrency(PlayerWallet.CurrencyType.Soft).ToString();
        }
    }
}
