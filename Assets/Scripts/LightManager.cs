using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : Interactable
{
    [SerializeField] GameObject affectedLight;
    [SerializeField] bool toggle = true;
    AudioSource lightSwitchAudioSource;
    [SerializeField] AudioClip lightSwitchAudioClip;


    private void Start()
    {
        lightSwitchAudioSource = GetComponent<AudioSource>();
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        ToggleLightsOn();
    }

    public override void OnLoseFocus()
    {
    }

    public void ToggleLightsOn()
    {
        toggle = !toggle;
        affectedLight.SetActive(toggle);
        lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
        Debug.Log("Light Toggled. Bool: " + toggle);
    }

    public void ForceLightsOff()
    {
        toggle = false;
        affectedLight.SetActive(toggle);
        lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
        Debug.Log("Light Forced Off. Bool: " + toggle);
    }
}
