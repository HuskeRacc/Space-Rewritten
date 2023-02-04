using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    [Header("Audio Source and Clips")]
    [SerializeField] AudioSource ambientAudioSource;
    [SerializeField] AudioClip[] ambienceSounds;

    [Header("RNG Values")]
    [SerializeField] int RNGHi = 20;
    [SerializeField] int RNGLo = 1;
    [SerializeField] int RNGResult;
    [SerializeField] int RNGTarget = 5;
    [SerializeField] int failedRNGHits = 0;

    [Header("Timers")]
    [SerializeField] float initialTimer = 60;
    [SerializeField] float cooldownTimer = 60;

    private void Start()
    {
        InvokeRepeating(nameof(AmbienceRNG), initialTimer, cooldownTimer);
    }

    private void AmbienceRNG()
    {
        RNGResult = Random.Range(RNGLo, RNGHi);
        ResultCheck();
    }

    private void ResultCheck()
    {
        if(RNGResult == RNGTarget)
        {
            failedRNGHits = 0;
            ambientAudioSource.PlayOneShot(ambienceSounds[Random.Range(0,ambienceSounds.Length)]);
        }
        else
        {
            failedRNGHits++;
            if(failedRNGHits >= 10)
            {
                ForcePlay();
            }
        }
    }

    private void ForcePlay()
    {
        failedRNGHits = 0;
        ambientAudioSource.PlayOneShot(ambienceSounds[Random.Range(0, ambienceSounds.Length)]);
    }
}
