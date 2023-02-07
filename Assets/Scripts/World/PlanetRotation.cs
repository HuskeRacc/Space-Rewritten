using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotSpeed = .1f;
    public static PlanetRotation Instance;

    private void Awake()
    {
        Instance = this;  
    }

    private void Update()
    {
        this.gameObject.transform.Rotate(Vector3.one * rotSpeed * Time.deltaTime);
    }
}
