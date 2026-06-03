using UnityEngine;

public class LightManager : Interactable
{
    [SerializeField] private GameObject[] affectedLights;
    [SerializeField] GameObject emergencyLight;

    [Header("State")]
    [SerializeField] private bool lightSwitchOn = true;
    [SerializeField] private bool hasPower = true;
    [SerializeField] private bool hasError = false;
    [SerializeField] bool hasEmergencyLight = false;

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

        if(affectedLights.Length == 0)
        {
            Debug.Log("Light switch " +  this.gameObject.name + " does not have lights assigned");
        }
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
        for (int i = 0; i < affectedLights.Length; i++)
        {
            if (affectedLights[i] != null)
            {
                affectedLights[i].SetActive(lightSwitchOn && hasPower && !hasError);
            }
        }
        if (hasEmergencyLight && emergencyLight != null)
        {
            emergencyLight.SetActive(!hasPower);
        }
    }
}