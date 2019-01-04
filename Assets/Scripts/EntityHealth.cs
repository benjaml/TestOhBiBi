using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100;
    private float _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(float amount)
    {
        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        if(_currentHealth == 0)
        {
            TriggerDeath();
        }
    }

    void TriggerDeath()
    {
        Destroy(this);
    }
    


}
