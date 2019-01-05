using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    private int _currentHealth;

    public delegate void DeathCallback();
    private DeathCallback _deathCallback = null;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.tag == "Player")
        {
            PlayerUI.Instance.NotifyHealth(_maxHealth);
        }
        else
        {
            _maxHealth = GetComponent<EntityInfos>().Health;
        }
        _currentHealth = _maxHealth;
    }

    public void SetDeathCallback(DeathCallback callback)
    {
        _deathCallback = callback;
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        if(_currentHealth == 0)
        {
            TriggerDeath();
        }
        if (transform.tag == "Player")
        {
            PlayerUI.Instance.NotifyHealth(_currentHealth);
        }
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
        if (transform.tag == "Player")
        {
            PlayerUI.Instance.NotifyHealth(_currentHealth);
        }
    }

    void TriggerDeath()
    {
        //Display retry if the player watch an add
        if (_deathCallback != null)
        {
            _deathCallback();
        }
    }
    


}
