using System;
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
    [SerializeField]
    private float _mobileAccelerationMagnitudeToReload;
    [SerializeField]
    private int _infiniteAmmoPowerUpDurationMinutes;


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
    private bool _isReloading = false;
    private bool _infiniteAmmoActivated = false;

    void Start()
    {
        _damage += PlayerPrefs.GetInt("Damage");
        CurrentAmmoCount = _MaxAmmo;
        _infiniteAmmoActivated = CheckInfiniteAmmo();
    }
    
    void Update()
    {
        if(_infiniteAmmoActivated)
        {
            _infiniteAmmoActivated = CheckInfiniteAmmo();
        }
        if(_isReloading)
        {
            return;
        }
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
                    if(!_infiniteAmmoActivated)
                    {
                        CurrentAmmoCount--;
                    }
                    if(_currentAmmoCount == 0)
                    {
                        StartCoroutine("Reload");
                    }
                }   
            }
        }
        if(_currentAmmoCount < _MaxAmmo)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine("Reload");
            }
            if(Input.acceleration.magnitude > _mobileAccelerationMagnitudeToReload)
            {
                StartCoroutine("Reload");
            }
        }
    }

    private bool CheckInfiniteAmmo()
    {
        if(PlayerPrefs.GetInt("InfiniteAmmo") == 1)
        {
            TimeSpan elapsedTime = (DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("InfiniteAmmoStart")));
            TimeSpan remainingTime = TimeSpan.FromMinutes(PlayerPrefs.GetInt("InfiniteAmmoDuration")) - elapsedTime;
            if (remainingTime <= new TimeSpan())
            {
                PlayerPrefs.SetInt("InfiniteAmmo", 0);
                return false;
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator Reload()
    {
        _isReloading = true;
        yield return new WaitForSeconds(_reloadTime);
        CurrentAmmoCount = _MaxAmmo;
        _isReloading = false;
    }
}
