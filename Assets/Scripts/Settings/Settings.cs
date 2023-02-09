using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensSlider;

    private void Start()
    {
        LoadVolumeSettings();
        LoadSensitivitySettings();
    }

    #region audio
    void LoadVolumeSettings()
    {
        if(PlayerPrefs.GetFloat("volume") != 0.00f)
        {
            mixer.SetFloat("GeneralVolume", PlayerPrefs.GetFloat("volume"));
        }
        else
        {
            mixer.SetFloat("GeneralVolume", 0.00f);
        }
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void SetAudioLevel(float _sliderValue)
    {
        mixer.SetFloat("GeneralVolume", _sliderValue);
        PlayerPrefs.SetFloat("volume", _sliderValue);
    }
    #endregion

    #region Sensitivity
    void LoadSensitivitySettings()
    {
        if(PlayerPrefs.GetFloat("sensitivity") != 0.00f)
        {
            PlayerMovement.instance.lookSpeed = PlayerPrefs.GetFloat("sensitivity");
            sensSlider.value = PlayerPrefs.GetFloat("sensitivity");
        }
        else
        {
            PlayerMovement.instance.lookSpeed = 1;
        }

    }

    public void SetSensitivity(float _sliderValue)
    {
        PlayerMovement.instance.lookSpeed = _sliderValue;
        PlayerPrefs.SetFloat("sensitivity", _sliderValue);
    }

    #endregion
}
