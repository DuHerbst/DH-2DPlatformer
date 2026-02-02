using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    // SPAWN POINT
    public Transform spawnPoint;
    
    //CHARACTER FEATURES
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float moveAcceleration = 10f;
    [SerializeField] private float moveDeceleration = 10f;
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
    public bool canClimb; // is the player currently climbing??
    
    //DASH CHECK
    [SerializeField] private float dashDistance; // how far the dash goes (unsure how to implement yet)
    [SerializeField] private float dashCooldown; // time between dashes
    private bool canDash = true;
    
    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }
    

    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance, groundLayer);
    }
    
    // void ClimbWallCheck()
    // {
    //     isClimbing = Physics2D.Raycast((Vector2)transform.position + climbStartPointOffset, Vector2.right, climbWallCheckDistance, climbLayer);
    // }
    
    void FixedUpdate() // to do movement is better to make everything in the fixed update method
    {
        HandleMovement();
        GroundCheck();
    }
    
    void OnEnable()
    {
        playerActions.OnJump += HandleJumpInput;
        playerActions.Move += HandleMoveInput;
        playerActions.Vertical += HandleClimbInput;
        playerActions.Pause += OnPause;
    }

    private void OnPause(float obj)
    {
        Debug.Log("PlayerController detected Pause input");
        // find one game manager in the scene and call its pause method
        
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.PauseGame();
        }
        
    }


    void OnDisable()
    { 
        playerActions.OnJump -= HandleJumpInput; 
        playerActions.Move -= HandleMoveInput;
        playerActions.Vertical -= HandleClimbInput;
        playerActions.Pause -= OnPause;
    }
    

    void HandleJumpInput()
        {
            
            if (isGrounded)
            {
                _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse); // Vertical force upwards
                isGrounded = false; // we are no longer grounded after jumping so then the character cant jump again
                Debug.Log("Jumped!");
            }
            
            else
            {
                Debug.Log("You are not grounded, cannot jump"); // add double jump logic here later
            }
            
        }
    
    void HandleMoveInput(float value)
    {
        //isGrounded = true;
        _moveInput = value;
    }
    
    
    void HandleMovement()
    {
        if (!_playerRb) return; 
        _playerRb.linearVelocityX = _moveInput * moveSpeed;
        
    }

    void HandleClimbInput (float value) // climbing logic - hopefully we can get this to work!
    {
        
        if (canClimb)
        {
            isGrounded = true;
            _moveInput = value;
            //_playerRb.gravityScale = 0;
            _playerRb.linearVelocityY = _moveInput * climbSpeed;
        }
        // else
        // {
        //     _playerRb.gravityScale = 2;
        // }
    }
    
    

    private void OnDrawGizmos()
    {
        Vector2 start = (Vector2)transform.position + startPointOffset; // it is probably better to add variables to everything so its better visually. Start point of the raycast
        Vector2 end = start + Vector2.down * groundCheckDistance; // end point of the raycast
        
        Debug.DrawLine(start, end, isGrounded ? Color.magenta : Color.red); // draw the line in the scene view, color changes based on if grounded or not
        
        Debug.DrawLine(start, end, canClimb ? Color.magenta : Color.red); // draw the line in the scene view, color changes based on if climbing or not
    }
    
}
