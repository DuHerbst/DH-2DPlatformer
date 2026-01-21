using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //CHARACTER FEATURES
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    //[SerializeField] private float climbSpeed = 5f;
    //[SerializeField] private float dashForce = 15f;
    //[SerializeField] private float crouchSpeed = 5f;
    
    // INPUT MANAGER
    
    [SerializeField] private PlayerMovement playerActions; // reference to the input actions component
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
        Vector2 origin = (Vector2)transform.position + startPointOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
    }

    private void OnEnable()
    {
        if (playerActions != null)
        {
            playerActions.OnJump += HandleJumpInput;
            playerActions.Move += HandleMoveInput;
        }
    }

    private void OnDisable()
    {
        if (playerActions != null)
        {
            playerActions.OnJump -= HandleJumpInput;
            playerActions.Move -= HandleMoveInput;
        }
    }

    void HandleJumpInput()
    {
        //Apply jump force here
        if (_playerRb == null) return;
        if (!isGrounded) return;

        _playerRb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

    void HandleMoveInput(float value)
    {
        _moveInput = value;
    }
    
    void FixedUpdate() // do physics in FixedUpdate
    {
        HandleMovement();
        GroundCheck();
    }
    
    void HandleMovement()
    {
        if (_playerRb == null) return;
        _playerRb.linearVelocity = new Vector2(_moveInput * moveSpeed, _playerRb.linearVelocity.y);
    }

    private void OnDrawGizmos()
    {
        Vector2 start = (Vector2)transform.position + startPointOffset;
        Vector2 end = start + Vector2.down * groundCheckDistance;
        Debug.DrawLine(start, end, isGrounded ? Color.magenta : Color.red);
    }
    
}
