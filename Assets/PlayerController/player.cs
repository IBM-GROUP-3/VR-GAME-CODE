using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class player : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] float acceleration = 20f;

    [SerializeField] float worldBottomBoundary = -100f;
    public Transform cameraTransform;

    public bool IsGrounded => controller.isGrounded;

    public float Height 
    {
        get => controller.height;
        set => controller.height = value;
    }
    public event Action OnBeforeMove;
    public event Action<bool> OnGroundStateChange;

    internal float movementSpeedMultiplier;


    CharacterController controller;
    internal Vector3 velocity;
    Vector2 look;

    (Vector3, Quaternion) initialPositionAndRotation;

    bool wasGrounded;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction lookAction;
    InputAction sprintAction;

    void Awake()
    {
        controller = GetComponent<CharacterController>();   
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        sprintAction = playerInput.actions["Sprint"];

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initialPositionAndRotation = (transform.position, transform.rotation);
    }
    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        Physics.SyncTransforms();
        look.x = rotation.eulerAngles.y;
        look.y = rotation.eulerAngles.z;
        velocity = Vector3.zero;
    }

    void Update()
    {
        UpdateGround();
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
        CheckBounds();
    }

    void CheckBounds()
    {
        if (transform.position.y < worldBottomBoundary)
        {
            var (position, rotation) = initialPositionAndRotation;
            Teleport(position, rotation);
        }
    }

    void UpdateGround()
    {
        if (wasGrounded != IsGrounded)
        {
            OnGroundStateChange?.Invoke(IsGrounded);
            wasGrounded = IsGrounded;
        }
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    Vector3 GetMovementInput ()
    {

        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        
        input *= moveSpeed * movementSpeedMultiplier;
        return input;
    }
    void UpdateMovement()
    {
        movementSpeedMultiplier = 1f;
        OnBeforeMove?.Invoke();

       var input = GetMovementInput();

        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateLook()
    {
        var lookInput = lookAction.ReadValue<Vector2>();
        look.x += lookInput.x * mouseSensitivity;
        look.y += lookInput.y * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
 