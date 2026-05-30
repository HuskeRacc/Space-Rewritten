using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggSystem : MonoBehaviour
{
    [SerializeField] GameObject itemToEnable;
    [SerializeField] bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("egg1") && !triggered)
        {
            itemToEnable.SetActive(true);
            triggered = true;
        }
    }
}
