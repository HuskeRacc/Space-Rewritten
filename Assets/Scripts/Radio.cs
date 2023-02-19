using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    AudioSource radioAudioSource;
    [SerializeField] AudioClip[] music;

    private void Start()
    {
        radioAudioSource = GetComponentInChildren<AudioSource>();
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        ToggleMusic();
    }

    public override void OnLoseFocus()
    {
    }

    void ToggleMusic()
    {
        if (!radioAudioSource.isPlaying)
        {
            radioAudioSource.clip = music[Random.Range(0, music.Length)];
            radioAudioSource.Play();
        }
        else
        {
            radioAudioSource.Stop();
        }

    }
}
