using System.Collections;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _rateOfFire;
    [SerializeField]
    private int _MaxAmmo;
    [SerializeField]
    private float _reloadTime;

    private int _currentAmmoCount;
    public int CurrentAmmoCount
    {
        get { return _currentAmmoCount; }
        set
        {
            _currentAmmoCount = value;
            PlayerUI.Instance.NotifyAmmoCount(_currentAmmoCount, _MaxAmmo);
        }
    }
    private float _lastShot;

    void Start()
    {
        CurrentAmmoCount = _MaxAmmo;
    }
    
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android || Input.GetMouseButton(0))
        {
            if (Time.time - _lastShot > _rateOfFire && _currentAmmoCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if(Application.platform == RuntimePlatform.WindowsEditor ||  hit.transform.tag == "Damageable")
                {
                    hit.collider?.GetComponent<EntityHealth>()?.TakeDamage(_damage);
                    _lastShot = Time.time;
                    CurrentAmmoCount--;
                    if(_currentAmmoCount == 0)
                    {
                        StartCoroutine("Reload");
                    }
                }   
            }
        }
    }

    private void Shoot(EntityHealth healthComponent)
    {
        healthComponent?.TakeDamage(_damage);
        _lastShot = Time.time;
        CurrentAmmoCount--;
        if (_currentAmmoCount == 0)
        {
            StartCoroutine("Reload");
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        CurrentAmmoCount = _MaxAmmo;
    }
}
