using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensSlider;
    [SerializeField] TextMeshProUGUI fpsValue;
    [SerializeField] bool fpsCounterToggled = false;

    [SerializeField] bool isCutscene = false;

    int lastFrameIndex;
    float[] frameDeltaTimeArray;

    private void Awake()
    {
        frameDeltaTimeArray = new float[50];
    }

    private void Start()
    {
        LoadFPSSettings();
        LoadVolumeSettings();
        LoadSensitivitySettings();
    }

    private void Update()
    {
        ManageFPSCounter();
    }

    #region audio

    private const string VolumePrefKey = "volume";
    private const string MixerVolumeParameter = "GeneralVolume";

    void LoadVolumeSettings()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);

        if(volumeSlider != null)
        {
            volumeSlider.minValue = 0.0001f;
            volumeSlider.maxValue = 1f;
            volumeSlider.value = savedVolume;
        }

        ApplyVolume(savedVolume);
    }

    public void SetAudioLevel(float _sliderValue)
    {
        _sliderValue = Mathf.Clamp(_sliderValue, 0.0001f, 1f);

        ApplyVolume(_sliderValue);

        PlayerPrefs.SetFloat(VolumePrefKey, _sliderValue);
        PlayerPrefs.Save();
    }

    private void ApplyVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);

        float volumeDb = Mathf.Log10(volume) * 20f;

        mixer.SetFloat(MixerVolumeParameter, volumeDb);
    }
    #endregion

    #region Sensitivity
    void LoadSensitivitySettings()
    {
        if(PlayerPrefs.GetFloat("sensitivity") != 0.00f)
        {
            if(!isCutscene)
                PlayerMovement.instance.lookSpeed = PlayerPrefs.GetFloat("sensitivity");

            sensSlider.value = PlayerPrefs.GetFloat("sensitivity");
        }
        else
        {
            PlayerMovement.instance.lookSpeed = 1;
            sensSlider.value = 1;
        }

    }

    public void SetSensitivity(float _sliderValue)
    {
        PlayerMovement.instance.lookSpeed = _sliderValue;
        PlayerPrefs.SetFloat("sensitivity", _sliderValue);
    }

    #endregion

    #region FPSCounter

    void LoadFPSSettings()
    {
        if(PlayerPrefs.GetInt("fpsCounterState") == 1)
        {
            fpsCounterToggled = true;
            ToggleFPS();
        }
        else
        {
            fpsCounterToggled = false;
            ToggleFPS();
        }
    }

    public void OnClick_ToggleFPSCounter()
    {
        fpsCounterToggled = !fpsCounterToggled;

        if (fpsCounterToggled)
            PlayerPrefs.SetInt("fpsCounterState", 1);
        else
            PlayerPrefs.SetInt("fpsCounterState", 0);

        ToggleFPS();
    }

    void ToggleFPS()
    {
        fpsValue.gameObject.SetActive(fpsCounterToggled);
    }

    void ManageFPSCounter()
    {

        if(!isCutscene)
        {
            if (!PlayerMovement.instance.isPaused)
            {
                frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
                lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
                fpsValue.text = Mathf.RoundToInt(CalculateFPS()).ToString() + "fps";
            }
        }
        else
        {
            frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
            lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
            fpsValue.text = Mathf.RoundToInt(CalculateFPS()).ToString() + "fps";
        }

    }

    float CalculateFPS()
    {
        float total = 0f;
        foreach (var deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }
    #endregion
}
