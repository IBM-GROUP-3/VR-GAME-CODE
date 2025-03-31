using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;

    // Distance at which the menu should appear in front of the player
    public float spawnDistance = 2f;

    public GameObject menu;

    // Input action that toggles the menu 
    public InputActionProperty showButton;

    // Update is called once per frame
    void Update()
    {
        // Check if the menu toggle button was pressed this frame
        if (showButton.action.WasPressedThisFrame())
        {
            // Toggle the menu's active state
            menu.SetActive(!menu.activeSelf);

            // Position the menu in front of the player's head at the specified distance
            menu.transform.position = head.position +
                new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        // Make the menu face the player
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));

        // Flip the menu's forward direction so it doesn't face backward
        menu.transform.forward *= -1;
    }
}
