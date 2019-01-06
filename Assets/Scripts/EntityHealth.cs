using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField]
    protected int _maxHealth;
    protected int _currentHealth;

    public delegate void DeathCallback();
    private DeathCallback _deathCallback = null;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void SetDeathCallback(DeathCallback callback)
    {
        _deathCallback = callback;
    }

    // Update is called once per frame
    public virtual void TakeDamage(int amount)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        if(_currentHealth == 0)
        {
            TriggerDeath();
        }
    }

    public virtual void Heal(float percentage)
    {
        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + (int)(percentage * _maxHealth));
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
