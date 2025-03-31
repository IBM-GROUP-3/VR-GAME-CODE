using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(player))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float jumpPressBufferTime = .05f;
    [SerializeField] float jumpGroundGraceTime = .2f;

    player player;

    bool tryingToJump;
    float lastJumpPressTime;
    float lastGroundedTime;
    void Awake()
    {
        player = GetComponent<player>(); 
    }

    void OnJump()
    {
        tryingToJump = true;
        lastJumpPressTime = Time.time;
    }

    void OnEnable()
    {
        player.OnBeforeMove += OnBeforeMove;
        player.OnGroundStateChange += OnGroundStateChange;
    }
    void OnDisable()
    {
        player.OnBeforeMove -= OnBeforeMove;
        player.OnGroundStateChange -= OnGroundStateChange;
    }
    void OnBeforeMove()
    {
        bool wasTryingToJump = Time.time - lastJumpPressTime < jumpPressBufferTime;
        bool wasGrounded = Time.time - lastGroundedTime < jumpGroundGraceTime;

        bool isOrWasTryingToJump = tryingToJump || (wasTryingToJump && player.IsGrounded);
        bool isOrWasGrounded = player.IsGrounded || wasGrounded;

        if (isOrWasTryingToJump && isOrWasGrounded) 
        { 
            player.velocity.y += jumpSpeed;
        }
        tryingToJump = false;
    }

    void OnGroundStateChange(bool isGrounded)
    {
        if (!isGrounded) lastGroundedTime = Time.time;
        
    }
}
