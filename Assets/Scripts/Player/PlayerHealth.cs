using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : EntityHealth
{
    [SerializeField]
    private Image _hitFeedback;
      
    public override void Start()
    {
        base.Start();
        PlayerUI.Instance.NotifyHealth(1f);
        Health += PlayerPrefs.GetInt("Health");
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        PlayerUI.Instance.NotifyHealth((float)_currentHealth/_maxHealth);
        StartCoroutine("FlashHitFeedback");
    }

    public override void Heal(float percentage)
    {
        base.Heal(percentage);
        PlayerUI.Instance.NotifyHealth((float)_currentHealth / _maxHealth);
    }

    IEnumerator FlashHitFeedback()
    {
        // flash a red Image for one frame to make hit feedback
        _hitFeedback.color = new Color(_hitFeedback.color.r, _hitFeedback.color.g, _hitFeedback.color.b, 0.8f);
        yield return null;
        _hitFeedback.color = new Color(_hitFeedback.color.r, _hitFeedback.color.g, _hitFeedback.color.b, 0f);
    }
}
