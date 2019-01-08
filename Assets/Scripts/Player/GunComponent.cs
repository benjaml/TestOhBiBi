using System;
using System.Collections;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    #region VisibleVariables
    [SerializeField]
    private int _damage;
    [SerializeField]
    private float _rateOfFire;
    [SerializeField]
    private int _MaxAmmo;
    [SerializeField]
    private float _reloadTime;
    // Shake strength to reload
    [SerializeField]
    private float _mobileAccelerationMagnitudeToReload;
    [SerializeField]
    private int _infiniteAmmoPowerUpDurationMinutes;
    // Feedbacks
    [SerializeField]
    private GameObject _decalPrefab;
    [SerializeField]
    private AudioClip _gunSound;
    [SerializeField]
    private AudioClip _reloadSound;
    [SerializeField]
    private GameObject _hitParticle;
    [SerializeField]
    private float _shakeDuration;
    [SerializeField]
    private float _shakeMagnitude;
    #endregion

    private Animator _muzzleFlashAnimator;
    private AudioSource _shotAudioSource;
    private ScreenShake _cameraShake;


    private int _currentAmmoCount;
    public int CurrentAmmoCount
    {
        get { return _currentAmmoCount; }
        set
        {
            _currentAmmoCount = value;
            // override setter to update the UI
            PlayerUI.Instance.NotifyAmmoCount(_currentAmmoCount, _MaxAmmo);
        }
    }
    private float _lastShot;
    private bool _isReloading = false;
    private bool _infiniteAmmoActivated = false;

    private const float A_LITTLE_FORWARD = 0.01f;

    void Start()
    {
        // bonus Damage bought in the upgrade menu
        _damage += PlayerPrefs.GetInt("Damage");
        CurrentAmmoCount = _MaxAmmo;
        _infiniteAmmoActivated = CheckInfiniteAmmo();
        _muzzleFlashAnimator = GetComponentInChildren<Animator>();
        _shotAudioSource = GetComponent<AudioSource>();
        _cameraShake = GetComponent<ScreenShake>();
    }
    
    void Update()
    {
        if(_infiniteAmmoActivated)
        {
            _infiniteAmmoActivated = CheckInfiniteAmmo();
        }
        if(!_isReloading)
        {
            TryShoot();
        }
        
    }

    private void TryShoot()
    {
        if (Application.platform == RuntimePlatform.Android || Input.GetMouseButton(0))
        {
            if (Time.time - _lastShot > _rateOfFire && _currentAmmoCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                if (Application.platform != RuntimePlatform.Android || hit.transform.tag == "Damageable")
                {
                    // this can me null if firing in sky on PC
                    if (hit.collider)
                    {
                        Instantiate(_hitParticle, hit.point, Quaternion.identity);
                        EntityHealth component = hit.collider.GetComponent<EntityHealth>();
                        if(component)
                        {
                            component.TakeDamage(_damage);
                        }
                        else
                        {
                            // Only spawn decal on non damageable entities (IE walls, ground)
                            Instantiate(_decalPrefab, hit.point + hit.normal * A_LITTLE_FORWARD, Quaternion.LookRotation(-hit.normal));
                        }
                    }
                    _lastShot = Time.time;
                    if (!_infiniteAmmoActivated)
                    {
                        CurrentAmmoCount--;
                    }
                    if (_currentAmmoCount == 0)
                    {
                        StartCoroutine("Reload");
                    }
                    // Feedbacks
                    // randomize pitch to avoid the sound so be all time the same
                    _shotAudioSource.pitch = UnityEngine.Random.Range(1f, 1.1f);
                    _shotAudioSource.PlayOneShot(_gunSound);
                    _muzzleFlashAnimator.SetTrigger("Fire");
                    StartCoroutine(_cameraShake.Shake(_shakeDuration, _shakeMagnitude));
                }
            }
        }
        if (_currentAmmoCount < _MaxAmmo)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // PC reload
                StartCoroutine("Reload");
            }
            if (Input.acceleration.magnitude > _mobileAccelerationMagnitudeToReload)
            {
                // mobile reload
                StartCoroutine("Reload");
            }
        }

    }

    private bool CheckInfiniteAmmo()
    {
        if(PlayerPrefs.GetInt("InfiniteAmmo") == 1)
        {
            TimeSpan elapsedTime = (DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("InfiniteAmmoStart")));
            if (elapsedTime >= TimeSpan.FromMinutes(PlayerPrefs.GetInt("InfiniteAmmoDuration")))
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
        _shotAudioSource.pitch = 1f;
        _shotAudioSource.PlayOneShot(_reloadSound);
        yield return new WaitForSeconds(_reloadTime);
        CurrentAmmoCount = _MaxAmmo;
        _isReloading = false;
    }
}
