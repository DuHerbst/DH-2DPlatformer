using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //CHARACTER FEATURES
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 15f;
    
    // INPUT MANAGER
    
    [SerializeField] private PlayerMovement playerActions; // reference to the input actions scriptable object
    private float _moveInput = 0;
    private Rigidbody2D _playerRb;
    
    //GROUND CHECK
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 startPointOffset;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    
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
    
    void FixedUpdate() // to do movement is better to make everything in the fixed update method
    {
        HandleMovement();
        GroundCheck();
    }
    
    void HandleMovement()
    {
        if (_playerRb == null) return;
        
        _playerRb.linearVelocityX = _moveInput * moveSpeed;

    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset + (Vector2)transform.position + startPointOffset, 
            Vector2.down * groundCheckDistance, isGrounded ? Color.purple : Color.red); // if is grounded is true, draw purple line, else draw red line
    }
    
}
