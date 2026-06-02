using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Radio : Interactable
{
    [SerializeField] AudioSource radioAudioSource;
    [SerializeField] GameObject audioSourceGameObject;
    [SerializeField] AudioClip[] music;
    [SerializeField] List<AudioClip> musicIndex;
    [SerializeField] AudioClip selectedClip;
    [SerializeField] AudioClip[] spookyClip;
    int i = 1;

    [SerializeField] bool spookyPlaying = false;

    [Header("Input")]
    [SerializeField] private InputActionReference nextSongAction;

    private void Start()
    {
        radioAudioSource = audioSourceGameObject.GetComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        if(!radioAudioSource.isPlaying)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }

        if (spookyPlaying)
        {
            StopMusicSpooky();
        }
    }

    public override void OnFocus()
    {
        if (radioAudioSource.isPlaying && nextSongAction.action.WasPressedThisFrame() && !spookyPlaying)
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

    void PlayMusic()
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
    }

    void StopMusic()
    {
        radioAudioSource.Stop();
    }

    public void PlayMusicSpooky()
    {
        radioAudioSource.Stop();
        selectedClip = spookyClip[Random.Range(0, spookyClip.Length)];
        radioAudioSource.PlayOneShot(selectedClip);
        selectedClip = null;
        spookyPlaying = true;
    }

    void StopMusicSpooky()
    {
        radioAudioSource.Stop();
        spookyPlaying = false;
    }
}
