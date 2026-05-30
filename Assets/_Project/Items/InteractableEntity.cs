using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : Interactable
{
    [SerializeField] private Items items;
    [SerializeField] int amountLeft = 100;

    [SerializeField] AudioSource impactSource;

    [SerializeField] bool interactable = true;

    [SerializeField] float velocityRequired = 1f;

    //0 is metal, 1 is soft.
    [SerializeField][Range(0, 1)] int entityMaterial;


    private void Start()
    {
        impactSource = GetComponent<AudioSource>();
        if (impactSource == null)
            Debug.LogError("No Audio Source on Impact");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > velocityRequired && !collision.gameObject.CompareTag("Player"))
        {
            if (entityMaterial == 0)
                impactSource.PlayOneShot(ImpactSoundContainer.instance.impactClipsMetal[Random.Range(0, ImpactSoundContainer.instance.impactClipsMetal.Length)]);
            if (entityMaterial == 1)
                impactSource.PlayOneShot(ImpactSoundContainer.instance.impactClipsSoft[Random.Range(0, ImpactSoundContainer.instance.impactClipsSoft.Length)]);
        }
    }

    public override void OnFocus()
    {
        if(items.food && interactable == true)
        {
            if (items.name == "Coffee")
            {
                if (amountLeft == 100) // 100 is default value.
                    amountLeft = items.amountLeft;
            }

            if (items.name == "MRE")
            {
                if (amountLeft == 100) // 100 is default value.
                    amountLeft = items.amountLeft;
            }

            if (items.name == "Donut")
            {
                if (amountLeft == 100) // 100 is default value.
                    amountLeft = items.amountLeft;
            }
        }
    }

    public override void OnInteract()
    {
        if(items.food && interactable == true)
        {
            if(items.name == "Coffee")
            {
                if (amountLeft > 1)
                {
                    PlayerNeeds.instance.FatigueIncrease(items.fatigueReduction);
                    PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[0]);
                    amountLeft--;
                }
                else if (amountLeft <= 1)
                {
                    Debug.Log("No charges left");
                    PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[0]);
                    PlayerNeeds.instance.FatigueIncrease(items.fatigueReduction);
                    DynamicCrosshair.instance.SmoothCrosshairDisable();
                    Destroy(this.gameObject);
                }
            }
            if (items.name == "MRE")
            {
                if (amountLeft > 1)
                {
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[0]);
                    amountLeft--;
                }
                else if (amountLeft <= 1)
                {
                    Debug.Log("No charges left");
                    PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[0]);
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    DynamicCrosshair.instance.SmoothCrosshairDisable();
                    Destroy(this.gameObject);
                }
            }

            if (items.name == "Donut")
            {
                if (amountLeft > 1)
                {
                    PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[0]);
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    amountLeft--;
                }
                else if (amountLeft <= 1)
                {
                    PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[0]);
                    Debug.Log("No charges left");
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    DynamicCrosshair.instance.SmoothCrosshairDisable();
                    Destroy(this.gameObject);
                }
            }

            if (items.name == "Battery")
            {
                PlayerNeeds.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(PlayerNeeds.instance.audioClips[1]);
                PlayerMovement.instance.flashlightBattery = 100;
                Destroy(this.gameObject);
            }
        }
    }

    public override void OnLoseFocus()
    {
        if(interactable == true)
        amountLeft = 100;
    }
}
