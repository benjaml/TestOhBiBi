using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    [SerializeField]
    private Button _watchAddButton;
    [SerializeField]
    private Button _spendHardCurrencyButton;

    private int _continueCost;
    private bool _canWatchAdd;

    public void Display(int continueCost, bool canWatchAdd)
    {
        // I had to set TimeScale to 0 as I do not destroy the player and enemies are continuing
        // to attack so we heard enemies attack during the continue screen
        Time.timeScale = 0;
        _continueCost = continueCost;
        AmbiantMusic.Instance.Pause();
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
        Time.timeScale = 1;
        AmbiantMusic.Instance.Resume();
        GameManager.Instance.ResetGame();
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        // When we choose to exit the game we need to reset TimeScale to 1
        Time.timeScale = 1;
    }
}
