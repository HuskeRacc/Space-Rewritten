using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Look Settings")]
    public float sensitivity = 0.5f;
    public float pitchClamp = 60f;

    [Header("Smoothing Settings")]
    public float smoothTime = 0.05f;

    [Header("Zoom Settings")]
    public float defaultFOV = 60f;
    public float maxZoomFOV = 20f;
    public float zoomSensitivity = 0.05f;
    public float zoomSmoothTime = 0.1f;

    private float pitch;
    private float yaw;
    private float targetPitch;
    private float targetYaw;
    private float pitchVelocity;
    private float yawVelocity;

    private Quaternion baseRotation;
    public bool canLook = true;

    private Camera cam;
    private float targetFOV;
    private float fovVelocity;

    void Start()
    {
        cam = GetComponent<Camera>();
        targetFOV = defaultFOV;

        baseRotation = transform.rotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // This ensures the camera smoothly zooms in/out even during movement transitions
        cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, targetFOV, ref fovVelocity, zoomSmoothTime);

        if (!canLook || Mouse.current == null) return;

        // --- LOOK LOGIC ---
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        targetYaw += mouseDelta.x * sensitivity;
        targetPitch -= mouseDelta.y * sensitivity;
        targetPitch = Mathf.Clamp(targetPitch, -pitchClamp, pitchClamp);

        yaw = Mathf.SmoothDamp(yaw, targetYaw, ref yawVelocity, smoothTime);
        pitch = Mathf.SmoothDamp(pitch, targetPitch, ref pitchVelocity, smoothTime);

        transform.rotation = baseRotation * Quaternion.Euler(pitch, yaw, 0f);

        // --- ZOOM LOGIC ---
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetFOV -= scroll * zoomSensitivity;
            targetFOV = Mathf.Clamp(targetFOV, maxZoomFOV, defaultFOV);
        }
    }

    public void SetBaseRotation(Quaternion newBase)
    {
        baseRotation = newBase;
        pitch = 0f;
        yaw = 0f;
        targetPitch = 0f;
        targetYaw = 0f;
    }
    public void ResetZoom()
    {
        targetFOV = defaultFOV;
    }
}