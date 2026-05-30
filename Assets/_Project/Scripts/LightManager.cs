using UnityEngine;

public class LightManager : Interactable
{
    [SerializeField] private GameObject affectedLight;

    [Header("State")]
    [SerializeField] private bool lightSwitchOn = true;
    [SerializeField] private bool hasPower = true;
    [SerializeField] private bool hasError = false;

    [Header("Audio")]
    private AudioSource lightSwitchAudioSource;
    [SerializeField] private AudioClip lightSwitchAudioClip;

    [Header("References")]
    [SerializeField] private PowerGenerator powerGenerator;

    private void Start()
    {
        lightSwitchAudioSource = GetComponent<AudioSource>();

        if (powerGenerator != null)
        {
            hasPower = powerGenerator.powerGeneratorActive;
        }

        ApplyLightState();
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if (!hasPower)
        {
            Debug.Log("Can't turn lights on with power off");
            return;
        }

        ToggleLightSwitch();
    }

    public override void OnLoseFocus()
    {
        Debug.Log("LightManager lost focus");
    }

    public void SetPower(bool powered)
    {
        hasPower = powered;
        ApplyLightState();
    }

    public void SetLightError(bool errorActive)
    {
        hasError = errorActive;
        ApplyLightState();
    }

    public void ToggleLightSwitch()
    {
        lightSwitchOn = !lightSwitchOn;
        ApplyLightState();

        if (lightSwitchAudioSource != null && lightSwitchAudioClip != null)
        {
            lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
        }

        Debug.Log("Light switch toggled. Switch On: " + lightSwitchOn +
              ", Has Power: " + hasPower +
              ", Error: " + hasError);
    }

    private void ApplyLightState()
    {
        if (affectedLight != null)
        {
            affectedLight.SetActive(lightSwitchOn && hasPower && !hasError);
        }
    }
}