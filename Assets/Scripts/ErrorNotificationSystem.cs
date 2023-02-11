using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorNotificationSystem : MonoBehaviour
{
    public static ErrorNotificationSystem instance;

    [SerializeField] AudioSource errorSoundSource;

    [SerializeField] AudioClip[] errorSounds; // max 2 for now

    public bool oxygenUpgradeBought = false;
    public bool generatorUpgradeBought = false;
    public bool solarUpgradeBought = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        errorSoundSource = this.GetComponent<AudioSource>();
    }

    public void OxygenError()
    {
        if(oxygenUpgradeBought)
        errorSoundSource.PlayOneShot(errorSounds[0]);
    }

    public void GeneratorError()
    {
        if(generatorUpgradeBought)
        errorSoundSource.PlayOneShot(errorSounds[1]);
    }

    public void SolarError()
    {
        if (solarUpgradeBought)
            errorSoundSource.PlayOneShot(errorSounds[2]);
    }
}
