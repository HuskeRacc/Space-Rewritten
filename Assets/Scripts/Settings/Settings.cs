using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensSlider;
    [SerializeField] Toggle fpsToggle;
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
            if(!isCutscene)
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
