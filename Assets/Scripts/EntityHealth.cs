using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public delegate void DeathCallback();

    [SerializeField]
    protected int _maxHealth;
    public int Health { get { return _maxHealth; } set { _maxHealth = value; _currentHealth = _maxHealth; } }
    [SerializeField]
    private AudioClip[] _hitSounds;


    protected int _currentHealth;
    private AudioSource _audioSource;
    private DeathCallback _deathCallback = null;

    public virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetDeathCallback(DeathCallback callback)
    {
        _deathCallback = callback;
    }

    public virtual void TakeDamage(int amount)
    {
        _audioSource.PlayOneShot(_hitSounds[Random.Range(0, _hitSounds.Length)]);
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

    protected virtual void TriggerDeath()
    {
        if (_deathCallback != null)
        {
            _deathCallback();
        }
    }
    


}
