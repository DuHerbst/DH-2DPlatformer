using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //CHARACTER FEATURES
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float dashForce = 15f;
    //[SerializeField] private float crouchSpeed = 5f;
    
    // INPUT MANAGER
    
    [SerializeField] private PlayerMovement playerActions; // reference to the input actions scriptable object
    private float _moveInput = 0;
    private Rigidbody2D _playerRb;
    
    //GROUND CHECK
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startPointOffset;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    
    //CLIMB CHECK
    [SerializeField] private LayerMask climbLayer;
    [SerializeField] private float climbWallCheckDistance; // how far the raycast checks for climbable walls
    [SerializeField] private Vector2 climbStartPointOffset; // offset for the raycast start point
    [SerializeField] private bool isClimbing; // is the player currently climbing??
    
    //DASH CHECK
    [SerializeField] private float dashDistance; // how far the dash goes (unsure how to implement yet)
    [SerializeField] private float dashCooldown; // time between dashes
    private bool canDash = true;
    
    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }
    
    private void GroundCheck()
    {
        if (isGrounded)
        {
            Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance, groundLayer); // this whole line has a value, can be true or false
        }

    }
    
    void FixedUpdate() // to do movement is better to make everything in the fixed update method
    {
        HandleMovement();
        GroundCheck();
    }
    
    private void OnEnable()
    {
        playerActions.OnJump += HandleJumpInput;
        playerActions.Move += HandleMoveInput;
    }
    
    private void OnDisable()
    { 
        playerActions.OnJump -= HandleJumpInput; 
        playerActions.Move -= HandleMoveInput;
    }
    
    void HandleJumpInput()
    {
        //Apply jump force here - do we need any of the values at the top if so?
        if (_playerRb == null)
        {
            return;
        }
    
        _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
    
    }
    
    void HandleMoveInput(float value)
    {
        _moveInput = value;
    }
    
    
    void HandleMovement()
    {
        if (!_playerRb) return;
    
        _playerRb.linearVelocityX = _moveInput * moveSpeed;
    
    }

    private void OnDrawGizmos()
    {
        Vector2 start = (Vector2)transform.position + startPointOffset; // it is probably better to add variables to everything so its better visually. Start point of the raycast
        Vector2 end = start + Vector2.down * groundCheckDistance; // end point of the raycast
        Debug.DrawLine(start, end, isGrounded ? Color.magenta : Color.red); // draw the line in the scene view, color changes based on if grounded or not
    }
    
}
