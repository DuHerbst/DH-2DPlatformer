using System;
using System.Collections;
using System.Collections.Generic;
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
    
    //HP AND LIVES
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    private int onHitDamage = 1;
    private bool isDead = false; // trigger death or game over
    private bool canTakeDamage = true;
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 0.5f;

    //public GameObject healthHearts []; // does this create an array of heart game objects in the UI? -- i dont think so... 
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
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

    // public PlayerController(GameObject healthHearts)
    // {
    //     this.healthHearts = healthHearts;
    // }

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>(); // to get the rigidbody component from this game object
        currentHealth = maxHealth;
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
        
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        Debug.Log("Game Manager found? " + (gameManager != null));
        
        if (gameManager == null) // make sure you know that ! is NEGATIVEEEE
        {
            return;
        }
        
        gameManager.TogglePause();
        Debug.Log("You pressed esc! " + gameManager.isPaused);
        
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
        _playerRb.linearVelocityX = _moveInput * moveSpeed; // whne we are talking about velocity += is always acceleration (review)
        
        
    }

    void HandleClimbInput (float value) // climbing logic - hopefully we can get this to work!
    {
        
        if (canClimb)
        {
            isGrounded = true;
            _moveInput = value;
            _playerRb.gravityScale = 0;
            _playerRb.linearVelocityY = _moveInput * climbSpeed;
        }
        
        //reapply gravity when not climbing --
    }
    
    

    private void OnDrawGizmos()
    {
        Vector2 start = (Vector2)transform.position + startPointOffset; // it is probably better to add variables to everything so its better visually. Start point of the raycast
        Vector2 end = start + Vector2.down * groundCheckDistance; // end point of the raycast
        
        Debug.DrawLine(start, end, isGrounded ? Color.magenta : Color.red); // draw the line in the scene view, color changes based on if grounded or not
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTakeDamage || isDead)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Hazards"))
        {
            TakeDamage(onHitDamage);
        }

        if (other.gameObject.CompareTag("HealthPack"))
        {
            HealPlayer(1);
            Destroy(other.gameObject);
        }
    }

    private void HealPlayer(int i)
    {
        // need to add healing logic here
        currentHealth += i;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // if current health after healing exceeds max health, set it to max health and not go past that
        
    }

    private void TakeDamage(int damage)
    {
        
        if (!canTakeDamage || isDead)
        {
            return;
        }
        
        currentHealth -= damage;
        
        // a math function: math f to show how it looks like? I am not sure - i think it goes like this: (clamps values between a minimum and maximum value)
       currentHealth = Mathf.Clamp (currentHealth - damage, 0, maxHealth);
       

        Debug.Log("Player took damage. Current health: " + currentHealth);
        
        //here we need to build the health UI updates
        // player takes damage
        // player loses 1 heart per hit
        // UI gets updated if current health is less than max
        // start with an array of hearts in the UI manager
        
        if (currentHealth <= 0)
        {
            
            isDead = true;
            Debug.Log("You dead uwu");
            FindFirstObjectByType<GameManager>().RestartGame();
            
        }
        
        if (currentHealth <= 0 && !isDead)
        {
            Die();
            return;
        }
        
        StartCoroutine(InvincibilityCooldown());
        
    }
    
    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");

        StartCoroutine(RespawnRoutine());

    }
    
    //create an update function to show the updates in the UI hearts
    // public UpdateHealthUI()
    // {
    //     for (int i = 0; i < healthHearts.Length; i);
    //
    //     {
    //         if (i < currentHealth)
    //         {
    //             healthHearts[i].sprite = fullHeart;
    //         }
    //         else
    //         {
    //             healthHearts[i].sprite = emptyHeart;
    //         }
    //     }
    // }
    
    private IEnumerator InvincibilityCooldown()
    {
        canTakeDamage = false;
        Debug.Log("Invincibility active");
        yield return new WaitForSeconds(invincibilityDuration);
        canTakeDamage = true;
        Debug.Log("Invincibility inactive now");
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        transform.position = respawnPoint.position; // this will move the player to the point
        currentHealth = maxHealth; // reset health
        isDead = false; // reset death

        StartCoroutine(InvincibilityCooldown());
        Debug.Log("The player has respawned!! Is this working now? ahgsjdjkadnbsn");

    }
    
}
