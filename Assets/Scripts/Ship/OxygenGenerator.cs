using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OxygenGenerator : Interactable
{

    public bool o2GeneratorActive;
    public bool o2GeneratorAvailable;

    [SerializeField] AudioSource toggleSource;
    [SerializeField] AudioClip toggleClip;

    [SerializeField] AudioSource OxygenAudioSource;

    [SerializeField] ShipSystems ship;

    [SerializeField] GameObject lightIndicator;

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        Toggleo2Generator();
    }

    public override void OnLoseFocus()
    {
    }

    private void Update()
    {
        HandleOxygen();
        HandleLight();
        HandleAudio();
    }

    public void Toggleo2Generator()
    {
        if (o2GeneratorAvailable)
        {
            o2GeneratorActive = !o2GeneratorActive;
            toggleSource.PlayOneShot(toggleClip);
        }
    }

    void HandleAudio()
    {
        if(o2GeneratorActive)
        {
            OxygenAudioSource.UnPause();
        }
        else
        {
            OxygenAudioSource.Pause();
        }
    }

    void HandleLight()
    {
        if(o2GeneratorActive)
            lightIndicator.GetComponent<Renderer>().material.color = Color.green;
        else lightIndicator.GetComponent<Renderer>().material.color = Color.red;
    }

    void HandleOxygen()
    {
        if (o2GeneratorActive)
        {
            ship.shipOxygen += Time.deltaTime;
        }
    }
}
