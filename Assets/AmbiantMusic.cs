using UnityEngine;

public class AmbiantMusic : MonoBehaviour
{

    public static AmbiantMusic Instance = null;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }
    }

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void Pause()
    {
        _source.Pause();
    }

    public void Resume()
    {
        _source.UnPause();
    }
}
