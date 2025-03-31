using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetTurnType : MonoBehaviour
{
    // References to the XR turn providers (Assigned in the Unity Inspector)
    public ActionBasedSnapTurnProvider snapTurn;
    public ActionBasedContinuousTurnProvider continuousTurn;

    // Sets the turn type based on the selected index
    public void SetTypeFromIndex(int index)
    {
        // Check if the required components are assigned
        if (snapTurn == null || continuousTurn == null)
        {
            Debug.LogError("Snap Turn or Continuous Turn is missing!");
            return;
        }

        //Small delay to allow Unity to process the change (prevents race conditions)
        StartCoroutine(EnableTurnType(index));
    }

    // Coroutine to switch turn types with a slight delay
    private IEnumerator EnableTurnType(int index)
    {
        yield return null; // Wait for a frame to allow Unity to update properly

        if (index == 0) // Continuous Turn
        {
            snapTurn.enabled = false;
            continuousTurn.enabled = true;
        }
        else if (index == 1) // Snap Turn
        {
            snapTurn.enabled = true;
            continuousTurn.enabled = false;
        }

        // Log the change for debugging
        Debug.Log($"Turn type switched to: {(index == 0 ? "Continuous" : "Snap")}");
    }
}
