using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetMovementType : MonoBehaviour
{
    // References to movement systems (Assigned in the Unity Inspector)
    public ActionBasedContinuousMoveProvider continuousMove; // Continuous movement (smooth movement)
    public TeleportationProvider teleportation; // Teleportation system
    public ActivateTeleportationRay teleportationRay; // Handles teleportation ray activation

    // References to teleportation ray objects (left and right hand)
    public GameObject leftRayTeleport;
    public GameObject rightRayTeleport;

    /// <summary>
    /// Switches between continuous movement and teleportation based on the selected value.
    /// </summary>
    /// <param name="movementValue">0 for Continuous, 1 for Teleport</param>
    public void SwitchMovement(int movementValue)
    {
        // Ensure all required movement components are assigned
        if (continuousMove == null || teleportation == null || teleportationRay == null)
        {
            Debug.LogError("Movement components are missing!");
            return;
        }

        if (movementValue == 0) // Continuous movement
        {
            EnableContinuous();
            DisableTeleport();
        }
        else if (movementValue == 1) // Teleportation
        {
            DisableContinuous();
            EnableTeleport();
        }

        // Log movement type for debugging
        Debug.Log($"Movement type switched to: {(movementValue == 0 ? "Continuous" : "Teleport")}");
    }


    public void DisableTeleport()
    {
        // Disables all the teleportation components
        leftRayTeleport.SetActive(false); 
        rightRayTeleport.SetActive(false); 
        teleportation.enabled = false; 
        teleportationRay.enabled = false; 
    }


    public void EnableTeleport()
    {
        // Enables all the teleportation components
        leftRayTeleport.SetActive(true); 
        rightRayTeleport.SetActive(true); 
        teleportation.enabled = true; 
        teleportationRay.enabled = true; 
    }

    public void DisableContinuous()
    {
        continuousMove.enabled = false;
    }

    public void EnableContinuous()
    {
        continuousMove.enabled = true;
    }
}
