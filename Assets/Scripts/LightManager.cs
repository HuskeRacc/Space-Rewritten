using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Interactable
{
    [SerializeField] GameObject affectedLight;
    [SerializeField] bool toggle = true;
    AudioSource lightSwitchAudioSource;
    [SerializeField] AudioClip lightSwitchAudioClip;

    [SerializeField] PowerGenerator powerGenerator;

    public static LightManager instance;

    private void Start()
    {
        lightSwitchAudioSource = GetComponent<AudioSource>();
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if(powerGenerator.powerGeneratorActive)
        {
            if (toggle)
                ToggleLightOff();
            else
                ToggleLightOn();
        }
        else
        {
            Debug.Log("Can't turn lights on with power off");
        }
    }

    public override void OnLoseFocus()
    {
    }

    public void ToggleLightOn()
    {
        toggle = !toggle;
        affectedLight.SetActive(toggle);
        lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
        Debug.Log("Light Toggled. Bool: " + toggle);
    }

    public void ToggleLightOff()
    {
        toggle = false;
        affectedLight.SetActive(toggle);
        lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
        Debug.Log("Light Forced Off. Bool: " + toggle);
    }
}
