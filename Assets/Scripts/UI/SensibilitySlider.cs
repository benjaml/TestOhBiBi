using UnityEngine;
using UnityEngine.UI;

public class SensibilitySlider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float sensibility = PlayerPrefs.GetFloat("Sensibility");
        if(sensibility == 0)
        {
            // Initialize to avoid to have to change the sensibility before playing
            // by default I set Sensibility to half
            PlayerPrefs.SetFloat("Sensibility", 0.5f);
            sensibility = 0.5f;
        }
        GetComponent<Slider>().value = sensibility;
        
    }

    public void SetSensibility(float sensibility)
    {
        PlayerPrefs.SetFloat("Sensibility", sensibility);
    }
}
