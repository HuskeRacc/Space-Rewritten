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

    [Header("Input")]
    [SerializeField] private InputActionReference nextSongAction;

    private void Start()
    {
        radioAudioSource = audioSourceGameObject.GetComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        ToggleMusic();
    }

    public override void OnFocus()
    {
        if (radioAudioSource.isPlaying && nextSongAction.action.WasPressedThisFrame())
        {
            NextSong();
        }
    }

    public override void OnLoseFocus()
    {
        Debug.Log("Radio Lost Focus");
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
            radioAudioSource.Stop();
        }
    }

    public void ToggleMusicSpooky()
    {
        radioAudioSource.Stop();
        selectedClip = spookyClip[Random.Range(0, spookyClip.Length)];
        radioAudioSource.PlayOneShot(selectedClip);
        selectedClip = null;
    }
}
