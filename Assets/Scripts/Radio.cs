using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    AudioSource radioAudioSource;
    [SerializeField] AudioClip[] music;
    [SerializeField] List<AudioClip> musicIndex;
    [SerializeField] AudioClip selectedClip;
    int i = 1;
    private void Start()
    {
        radioAudioSource = GetComponentInChildren<AudioSource>();
    }

    public override void OnInteract()
    {
        ToggleMusic();
    }

    public override void OnFocus()
    {
        if (radioAudioSource.isPlaying && Input.GetKeyDown(KeyCode.R))
        {
            NextSong();
        }
    }

    public override void OnLoseFocus()
    {
        
    }

    void NextSong()
    {
        if (i >= music.Length)
        {
            musicIndex.Clear();
            i = 0;
        }

        selectedClip = music[i];
        musicIndex.Add(selectedClip);
        i += 1;

        radioAudioSource.clip = selectedClip;
        radioAudioSource.Play();
    }

    void ToggleMusic()
    {
        if (!radioAudioSource.isPlaying)
        {
            if (selectedClip == null)
            {
                selectedClip = music[0];
                radioAudioSource.Play();
            }
            else
            {
                radioAudioSource.clip = selectedClip;
                radioAudioSource.Play();
            }
        }
        else
        {
            radioAudioSource.Pause();
        }
    }
}
