using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] float rotSpeed = .1f;

    private void Update()
    {
        this.gameObject.transform.Rotate(Vector3.one * rotSpeed * Time.deltaTime);
    }
}
