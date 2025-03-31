using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(player))]
public class PlayerSprinting : MonoBehaviour
{
    [SerializeField] float speedMultiplier = 2f;
    player player;
    PlayerInput playerInput;
    InputAction sprintAction;

    void Awake()
    {
        player = GetComponent<player>();
        playerInput = GetComponent<PlayerInput>();
        sprintAction = playerInput.actions["sprint"];
    }

    void OnEnable() => player.OnBeforeMove += OnBeforeMove;
    void OnDisable() => player.OnBeforeMove -= OnBeforeMove;
    void OnBeforeMove()
    {
        var sprintInput = sprintAction.ReadValue<float>();
        player.movementSpeedMultiplier *= sprintInput > 0 ? speedMultiplier : 1f;
    }
}
