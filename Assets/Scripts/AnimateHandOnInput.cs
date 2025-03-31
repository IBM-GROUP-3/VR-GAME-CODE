using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    // Input action for detecting pinch (trigger button) input
    public InputActionProperty pinchAnimationAction;

    // Input action for detecting grip (grip button) input
    public InputActionProperty gripAnimationAction;

    // Reference to the Animator component that controls hand animations
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        // Read the current trigger (pinch) input value (range: 0 to 1)
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();

        // Set the "Trigger" parameter in the Animator based on the trigger input
        handAnimator.SetFloat("Trigger", triggerValue);

        // Read the current grip input value (range: 0 to 1)
        float gripValue = gripAnimationAction.action.ReadValue<float>();

        // Set the "Grip" parameter in the Animator based on the grip input
        handAnimator.SetFloat("Grip", gripValue);
    }
}
