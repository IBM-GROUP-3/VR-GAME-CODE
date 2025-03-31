using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    // References to the teleportation ray GameObjects for the left and right controllers
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    // Input actions to detect when the teleportation activation button is pressed
    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    // Input actions to detect when teleportation is canceled (e.g., when gripping an object)
    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    // XRRayInteractor components for detecting if the teleportation rays are hovering over a valid surface
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    // Update is called once per frame
    void Update()
    {
        // Check if the left ray is currently hovering over a valid teleportation surface
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);

        /* Enable the left teleportation ray only if:
        - The ray is NOT hovering over an interactive object
        - The cancel action is NOT active (meaning the player is not grabbing an object)
        - The activate action is pressed with a value greater than 0.1 (indicating a teleportation attempt)
        */
        leftTeleportation.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);

        // Check if the right ray is currently hovering over a valid teleportation surface
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);

        // Apply the same logic for the right teleportation ray as the left
        rightTeleportation.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
