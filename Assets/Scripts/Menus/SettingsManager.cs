using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] TextMeshProUGUI sensText;
    [SerializeField] TextMeshProUGUI volText;

    [SerializeField] Slider volSlider;
    [SerializeField] Slider sensSlider;

    float savedSens;
    float savedVol;

    private void Start()
    {
        LoadSettings();
        LoadVisuals();
    }

    void LoadSettings()
    {
        savedSens = PlayerPrefs.GetFloat("sensitivity");
        savedVol = PlayerPrefs.GetFloat("volume");

        if (savedSens != 0 && savedVol != 0)
        {
            playerMovement.lookSpeed = savedSens;
            audioMixer.SetFloat("volume", savedVol);
        }
    }

    void LoadVisuals()
    {
        sensSlider.value = savedSens;
        volSlider.value = savedVol;
    }


    private void Update()
    {
        UpdateVisuals();
    }

    void UpdateVisuals()
    {
        volText.text = volSlider.value.ToString();
        sensText.text = sensSlider.value.ToString();
    }

    public void Settings_SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);

        PlayerPrefs.SetFloat("volume", volume);
    }

    public void Settings_SetSensitivity(float sensitivity)
    {
        playerMovement.lookSpeed = sensitivity;

        PlayerPrefs.SetFloat("sensitivity", sensitivity);
    }
}
