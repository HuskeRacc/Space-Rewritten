using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.SceneView;

public class PlayerPointMovement : MonoBehaviour
{
    [Header("Setup")]
    public MoveNode currentNode;      // Start this as 'MiddlePoint' in the inspector
    public Transform cameraTransform; // Drag your Main Camera here
    public CameraMover cameraMover;   // Your transition script from earlier

    [Header("Settings")]
    [Tooltip("How perfectly aligned the camera needs to be with a node. 0.5 is a wide 90-degree cone. 0.8 is a stricter 36-degree cone.")]
    public float alignmentThreshold = 0.5f;

    void Update()
    {
        // Don't check for input if we are already moving
        if (Keyboard.current == null || cameraMover.isMoving) return;

        Vector3 desiredDirection = Vector3.zero;

        // Figure out which way the player WANTS to go based on the camera
        if (Keyboard.current.wKey.wasPressedThisFrame)
            desiredDirection = cameraTransform.forward;
        else if (Keyboard.current.sKey.wasPressedThisFrame)
            desiredDirection = -cameraTransform.forward;
        else if (Keyboard.current.aKey.wasPressedThisFrame)
            desiredDirection = -cameraTransform.right;
        else if (Keyboard.current.dKey.wasPressedThisFrame)
            desiredDirection = cameraTransform.right;

        // If a movement key was pressed this frame
        if (desiredDirection != Vector3.zero)
        {
            TryMoveInDirection(desiredDirection);
        }
    }

    private void TryMoveInDirection(Vector3 desiredDirection)
    {
        // 1. Flatten the Y axis! 
        // We do this so if the player is looking down at the floor, 
        // pressing 'W' still moves them horizontally into the next room.
        desiredDirection.y = 0;
        desiredDirection.Normalize();

        MoveNode bestNode = null;
        float highestDot = alignmentThreshold;

        // 2. Loop through all the neighbors of our CURRENT node
        foreach (MoveNode neighbor in currentNode.connectedNodes)
        {
            // Get the physical direction from where we are to the neighbor
            Vector3 directionToNeighbor = neighbor.transform.position - currentNode.transform.position;
            directionToNeighbor.y = 0; // Flatten this too
            directionToNeighbor.Normalize();

            // 3. The Dot Product magic
            float dot = Vector3.Dot(desiredDirection, directionToNeighbor);

            // If this node is in the right direction AND is the best match so far
            if (dot > highestDot)
            {
                highestDot = dot;
                bestNode = neighbor;
            }
        }

        // 4. If we found a valid node, trigger the movement!
        if (bestNode != null)
        {
            Debug.Log("Moving to: " + bestNode.gameObject.name);
            currentNode = bestNode; // Update our current location
            cameraMover.MoveToNode(bestNode.transform); // Tell the camera to slide over
        }
        else
        {
            Debug.Log("No connected node in that direction!");
        }
    }
}