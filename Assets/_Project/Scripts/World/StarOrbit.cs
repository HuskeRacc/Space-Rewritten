using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StarOrbit : MonoBehaviour
{
    public static StarOrbit instance;

    [SerializeField] float xSpread;
    [SerializeField] float zSpread;
    [SerializeField] float yOffset;
    [SerializeField] Transform centerPoint;
    [SerializeField] Transform sunLight;

    public float orbitSpeed;
    public float savedOrbitSpeed;
    public float sleepingOrbitSpeed;
    [SerializeField] bool orbitClockwise;

    float timer = 0;

    private void Awake()
    {
        savedOrbitSpeed = orbitSpeed;
        sleepingOrbitSpeed = orbitSpeed * 2;
        instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime * orbitSpeed;
        Orbit();
        sunLight.LookAt(centerPoint);
    }

    void Orbit()
    {
        if (orbitClockwise)
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new(x, yOffset, z);
            transform.position = pos + centerPoint.position;
        }
        else
        {
            float x = Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new(x, yOffset, z);
            transform.position = pos + centerPoint.position;
        }
    }
}
