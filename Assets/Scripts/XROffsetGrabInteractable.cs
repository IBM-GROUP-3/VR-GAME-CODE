using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    // Stores the initial local position and rotation of the attachTransform
    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;

    // Start is called before the first frame update
    void Start()
    {
        // If no attachTransform is assigned, create a new one at the object's current position
        if (!attachTransform)
        {
            // Create a new GameObject to serve as the offset grab pivot
            GameObject attachPoint = new GameObject("Offset Grab Pivot");

            // Set it as a child of this object without changing its local transform
            attachPoint.transform.SetParent(transform, false);

            // Assign the new transform as the attachTransform
            attachTransform = attachPoint.transform;
        }
        else
        {
            // Store the initial local position and rotation if an attachTransform already exists
            initialLocalPos = attachTransform.localPosition;
            initialLocalRot = attachTransform.localRotation;
        }
    }

    // Called when the object is grabbed
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // If the interactor is a direct hand grab (XRDirectInteractor), move the attachTransform to match the hand position
        if (args.interactorObject is XRDirectInteractor)
        {
            attachTransform.position = args.interactorObject.transform.position;
            attachTransform.rotation = args.interactorObject.transform.rotation;
        }
        else
        {
            // Otherwise, reset the attachTransform to its initial position and rotation
            attachTransform.localPosition = initialLocalPos;
            attachTransform.localRotation = initialLocalRot;
        }

        // Call the base method to ensure normal grab behavior continues
        base.OnSelectEntered(args);
    }
}
