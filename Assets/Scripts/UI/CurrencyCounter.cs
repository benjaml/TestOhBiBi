using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{

    private Text _text;
    [SerializeField]
    private bool _hardCurrency;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_hardCurrency)
        {
            _text.text = PlayerWallet.Instance.GetHardCurrency().ToString();
        }
        else
        {
            _text.text = PlayerWallet.Instance.GetSoftCurrency().ToString();
        }
    }
}
