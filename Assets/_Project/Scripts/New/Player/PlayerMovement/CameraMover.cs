using System.Collections;
using UnityEngine;
using UnityEngine.LowLevel;

public class CameraMover : MonoBehaviour
{
    [Header("Transition Settings")]
    [Tooltip("How fast the camera rotates to face the destination.")]
    public float panSpeed = 3.5f;
    [Tooltip("How fast the camera actually moves forward.")]
    public float moveSpeed = 2f;

    public bool isMoving { get; private set; } = false;

    private PlayerLook playerLook;

    void Start()
    {
        playerLook = GetComponent<PlayerLook>();
    }

    public void MoveToNode(Transform targetNode)
    {
        if (!isMoving)
        {
            StartCoroutine(TransitionRoutine(targetNode));
        }
    }

    private IEnumerator TransitionRoutine(Transform targetNode)
    {
        isMoving = true;

        if (playerLook != null)
        {
            playerLook.canLook = false;
            playerLook.ResetZoom();
        }

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        // Figure out the physical direction we need to travel
        Vector3 travelDirection = targetNode.position - startPos;
        travelDirection.y = 0;

        Quaternion targetRotation;
        if (travelDirection != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(travelDirection);
        }
        else
        {
            targetRotation = targetNode.rotation;
        }

        // --- PHASE 1: PANNING (ROTATION) ---
        float panProgress = 0f;
        while (panProgress < 1f)
        {
            panProgress += Time.deltaTime * panSpeed;
            float ease = Mathf.SmoothStep(0f, 1f, panProgress);

            // Only update rotation here
            transform.rotation = Quaternion.Lerp(startRot, targetRotation, ease);

            yield return null;
        }
        transform.rotation = targetRotation; // Hard snap at the end of Phase 1

        // Add a tiny pause here if you want it to feel more deliberate!
        //yield return new WaitForSeconds(0.1f);

        // --- PHASE 2: MOVING (POSITION) ---
        float moveProgress = 0f;
        while (moveProgress < 1f)
        {
            moveProgress += Time.deltaTime * moveSpeed;
            float ease = Mathf.SmoothStep(0f, 1f, moveProgress);

            // Only update position here
            transform.position = Vector3.Lerp(startPos, targetNode.position, ease);

            yield return null;
        }
        transform.position = targetNode.position; // Hard snap at the end of Phase 2

        // Set our new "center" view to the direction we just traveled
        if (playerLook != null)
        {
            playerLook.SetBaseRotation(targetRotation);
            playerLook.canLook = true;
        }

        isMoving = false;
    }
}