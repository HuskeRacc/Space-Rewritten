using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionManager : MonoBehaviour
{
    [Header("Optimization Settings")]
    [Tooltip("Stops the raycast from traveling to infinity.")]
    public float maxInteractionDistance = 10f;

    [Tooltip("By default, this is Layer 9, which your Interactable.cs sets automatically!")]
    public LayerMask interactableLayer = 1 << 9;

    private Camera playerCam;
    private Interactable currentInteractable;

    void Start()
    {
        playerCam = GetComponent<Camera>();

        if (playerCam == null)
            playerCam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        HandleInteractionCheck();
        HandleInteractionInput();
    }

    private void HandleInteractionCheck()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        // OPTIMIZATION: We now pass the max distance and the specific layer mask!
        // The physics engine will completely ignore walls, floors, and anything not on Layer 9.
        if (Physics.Raycast(ray, out RaycastHit hit, maxInteractionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out Interactable interactableObj))
            {
                DynamicCrosshair.instance.SmoothCrosshairEnable();

                if (currentInteractable != interactableObj)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnLoseFocus();

                    currentInteractable = interactableObj;
                    currentInteractable.OnFocus();
                }
                else
                {
                    currentInteractable.OnFocus();
                }
            }
        }
        else
        {
            // If the raycast didn't hit anything on Layer 9 within our max distance
            if (currentInteractable != null)
            {
                DynamicCrosshair.instance.SmoothCrosshairDisable();
                currentInteractable.OnLoseFocus();
                currentInteractable = null;
            }
        }
    }

    private void HandleInteractionInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }
}