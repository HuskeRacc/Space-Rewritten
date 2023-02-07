using System.Collections;
using UnityEngine;

public class DoorScript : Interactable
{
    private bool isOpen = false;
    [SerializeField] bool openable = true;
    private Animator anim;
    private AudioSource doorAudioSource;
    private GameObject parentDoor;
    [SerializeField] AudioClip open;
    [SerializeField] AudioClip close;
    bool interactedWith = false;

    private void Start()
    {
        GameObject parent = this.transform.parent.gameObject;

        doorAudioSource = parent.GetComponentInChildren<AudioSource>();
        parentDoor = this.transform.parent.gameObject;
        anim = parentDoor.GetComponentInChildren<Animator>();
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if(openable)
        {
            isOpen = !isOpen;
            anim.SetBool("isOpen", isOpen);
            interactedWith = true;
            if (isOpen && interactedWith)
            {
                OpenDoorAudio();
                StartCoroutine(AutoClose());
            }
            else if (!isOpen && interactedWith)
            {

                CloseDoorAudio();
            }
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Locked!", 2, false));
        }
    }

    public override void OnLoseFocus()
    {
    }

    IEnumerator AutoClose()
    {
        while(isOpen)
        {
            yield return new WaitForSeconds(3);

            if(Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) > 3)
            {
                isOpen = false;
                anim.SetBool("isOpen", isOpen);
            }
        }
    }

    void OpenDoorAudio()
    {
        doorAudioSource.clip = open;
        doorAudioSource.Play(0);
    }

    void CloseDoorAudio()
    {
        doorAudioSource.clip = close;
        doorAudioSource.Play(0);
    }
}
