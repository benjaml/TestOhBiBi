using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    private int _continueCost;
    private bool _canWatchAdd;


    [SerializeField]
    private Button _watchAddButton;
    [SerializeField]
    private Button _spendHardCurrencyButton;

    public void Display(int continueCost, bool canWatchAdd)
    {
        _continueCost = continueCost;
        _spendHardCurrencyButton.interactable = PlayerWallet.Instance.GetCurrency(PlayerWallet.CurrencyType.Hard) >= _continueCost;
        _spendHardCurrencyButton.GetComponentInChildren<Text>().text = _continueCost.ToString();
        _canWatchAdd = canWatchAdd;
        _watchAddButton.interactable = _canWatchAdd;
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void WatchAdd()
    {
        // Watch add. Not implemented for the test
        ResetGame();
    }

    public void SpendHardCurrencyToContinue()
    {
        PlayerWallet.Instance.AddCurrency(PlayerWallet.CurrencyType.Hard, -_continueCost);
        ResetGame();
    }

    private void ResetGame()
    {
        GameManager.Instance.ResetGame();
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
}
