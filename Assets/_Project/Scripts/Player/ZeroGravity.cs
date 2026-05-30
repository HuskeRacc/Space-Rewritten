using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravity : MonoBehaviour
{
    bool triggered = false;
    float savedGravity;

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("eva"))
        {
            if(!triggered)
            {
                triggered = true;
                DisableGravity();
            }
            else
            {
                triggered = false;
                EnableGravity();
            }
        }
    }

    void DisableGravity()
    {
        savedGravity = PlayerMovement.instance.gravity;
        PlayerMovement.instance.gravity = 0;
    }

    void EnableGravity()
    {
        PlayerMovement.instance.gravity = savedGravity;
    }

    private void Start()
    {

    }

}
