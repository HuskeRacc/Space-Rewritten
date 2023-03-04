using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OxygenGenerator : Interactable, ISaveable
{

    public bool o2GeneratorActive;
    public bool o2GeneratorAvailable;

    [SerializeField] AudioSource toggleSource;
    [SerializeField] AudioClip toggleClip;

    [SerializeField] AudioSource OxygenAudioSource;

    [SerializeField] ShipSystems ship;

    [SerializeField] GameObject lightIndicator;

    [SerializeField] Animator anim;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

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
        HandleAnimation();
    }

    void HandleAnimation()
    {
        if (o2GeneratorActive)
            anim.SetBool("togglestate", true);
        else
            anim.SetBool("togglestate", false);
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

    public object CaptureState()
    {
        return new SaveData
        {
            savedOxygenStatus = o2GeneratorActive
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        o2GeneratorActive = saveData.savedOxygenStatus;
    }

    [Serializable]
    private struct SaveData
    {
        public bool savedOxygenStatus;
    }
}
