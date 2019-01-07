using UnityEngine;

public class EnemyHealth : EntityHealth
{
    [SerializeField]
    private GameObject _healthBar;
    private float _baseWidth;

    // I've found this constant by playing with unity but I can't explain it
    private const float OFFSET_POSITION_MULTIPLIER = 4f;

    // Start is called before the first frame update
    public void Start()
    {
        _currentHealth = Health;
        _baseWidth = _healthBar.transform.localScale.x;
    }

    // Update is called once per frame
    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        UpdateLifeBar((float)_currentHealth/_maxHealth);
    }

    public override void Heal(float percentage)
    {
        base.Heal(percentage);
        UpdateLifeBar((float)_currentHealth / _maxHealth);
    }

    void UpdateLifeBar(float percentage)
    {
        float offset = ((1 - percentage) * _baseWidth) * OFFSET_POSITION_MULTIPLIER;
        _healthBar.transform.localPosition = -offset * Vector3.right;
        _healthBar.transform.localScale = new Vector3(_baseWidth * percentage, _healthBar.transform.localScale.y, _healthBar.transform.localScale.z);
    }

    private void Update()
    {
        _healthBar.transform.parent.transform.rotation = Camera.main.transform.rotation;
    }
}
