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
        ToggleLights();
    }

    public override void OnLoseFocus()
    {
    }

    public void ToggleLights()
    {
        toggle = !toggle;
        affectedLight.SetActive(toggle);
        lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
    }

    public void ForceLightsOff()
    {
        toggle = false;
        affectedLight.SetActive(toggle);
        lightSwitchAudioSource.PlayOneShot(lightSwitchAudioClip);
    }
}
