using UnityEngine;

public class PlayerHealth : EntityHealth
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        PlayerUI.Instance.NotifyHealth(1f);
        Health += PlayerPrefs.GetInt("Health");
    }

    // Update is called once per frame
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        PlayerUI.Instance.NotifyHealth((float)_currentHealth/_maxHealth);
    }

    public override void Heal(float percentage)
    {
        base.Heal(percentage);
        PlayerUI.Instance.NotifyHealth((float)_currentHealth / _maxHealth);
    }
}
