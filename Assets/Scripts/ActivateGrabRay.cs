using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    // References to the GameObjects representing the grab rays for left and right controllers
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;

    // References to the XRDirectInteractor components for detecting direct grabs
    public XRDirectInteractor leftDirectGrab;
    public XRDirectInteractor rightDirectGrab;

    // Update is called once per frame
    void Update()
    {
        // Enable the left grab ray if no objects are currently being grabbed with the left hand
        leftGrabRay.SetActive(leftDirectGrab.interactablesSelected.Count == 0);

        // Enable the right grab ray if no objects are currently being grabbed with the right hand
        rightGrabRay.SetActive(rightDirectGrab.interactablesSelected.Count == 0);
    }
}
