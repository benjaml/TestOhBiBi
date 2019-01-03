using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounter : MonoBehaviour
{

    private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = PlayerPrefs.GetInt("Currency").ToString();
    }
}
