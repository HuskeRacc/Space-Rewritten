using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;

    private void LateUpdate()
    {
        transform.rotation = playerCamera.rotation;
    }
}
