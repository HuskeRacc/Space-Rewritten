using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDESTOYER : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("asteroid"))
        {
            Debug.Log("ASTEROID!!!!!!!!!!!!!");
            Destroy(other.gameObject);
        }
    }
}
