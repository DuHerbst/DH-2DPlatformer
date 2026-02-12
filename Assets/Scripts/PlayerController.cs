using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // AUDIO
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip healSound;
    
    //CHARACTER FEATURES
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float climbSpeed = 5f;
    
    //HP AND LIVES
    [SerializeField] private int maxHealth = 3;
    private int _currentHealth;
    private int _onHitDamage = 1;
    private bool _isDead = false; // trigger death or game over
    private bool _canTakeDamage = true;
    
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 0.5f;

    public Image[] healthHearts; // does this create an array of heart game objects in the UI?
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    // INPUT MANAGER
    [SerializeField] private PlayerMovement playerActions; // reference to the input actions scriptable object
    private float _moveInputX = 0f;
    private float _moveInputY = 0f;
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
    public bool canClimb; // I left this public so Game Manager has access to it?
    

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>(); // to get the rigidbody component from this game object
        _currentHealth = maxHealth;
        UpdateHealthUI();
    }
    

    void GroundCheck()
    {
        isGrounded = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance, groundLayer);
    }
    
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
        
        GameManager gameManager = GameManager.Instance;
        
        if (gameManager == null)
        {
            return;
        }
        
        gameManager.TogglePause();
        
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
                audioSource.PlayOneShot(jumpSound); // Play sound effect for jumping
                
            }
            
        }
    
    void HandleMoveInput(float value)
    {
        //isGrounded = true;
        _moveInputX = value;
    }
    
    
    void HandleMovement()
    {
        if (!_playerRb) return; 
        _playerRb.linearVelocityX = _moveInputX * moveSpeed; // whne we are talking about velocity += is always acceleration (review)
        
        
    }

    void HandleClimbInput (float value) // climbing logic - hopefully we can get this to work!
    {
        
        if (canClimb)
        {
            isGrounded = true;
            _moveInputY = value;
            _playerRb.gravityScale = 0;
            _playerRb.linearVelocityY = _moveInputY * climbSpeed;
        }

        else
        {
            _playerRb.gravityScale = 2;
        }
        
    }
    

    private void OnDrawGizmos()
    {
        Vector2 start = (Vector2)transform.position + startPointOffset; // it is probably better to add variables to everything so its better visually. Start point of the raycast
        Vector2 end = start + Vector2.down * groundCheckDistance; // end point of the raycast
        
        Debug.DrawLine(start, end, isGrounded ? Color.magenta : Color.red); // draw the line in the scene view, color changes based on if grounded or not
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canTakeDamage || _isDead)
        {
            return;
        }
        
        if (other.gameObject.CompareTag("Hazards"))
        {
            TakeDamage(_onHitDamage);
        }

        if (other.gameObject.CompareTag("HealthPack"))
        {
            HealPlayer(1);
            UpdateHealthUI();
            Destroy(other.gameObject);
        }
    }

    private void HealPlayer(int i)
    {
        _currentHealth += i;
        audioSource.PlayOneShot(healSound); // Play sound effect for healing taken
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth); // if current health after healing exceeds max health, set it to max health and not go past that
        
    }

    private void TakeDamage(int damage)
    {
        
        if (!_canTakeDamage || _isDead)
        {
            return;
        }
        
        _currentHealth = Mathf.Clamp (_currentHealth - damage, 0, maxHealth); // to reduce health up to 0
        
        if (_currentHealth <= 0 && !_isDead) // if the players health is 0 AND ITS NOT DEAD (!)
        {
            Die();
            return;
        }
        
        audioSource.PlayOneShot(damageSound); // Play sound effect for damage taken
        UpdateHealthUI();
        StartCoroutine(InvincibilityCooldown());
        
    }
    
    private void Die()
    {
        _isDead = true;
        audioSource.PlayOneShot(deathSound); // Play sound effect for damage taken
        StartCoroutine(RespawnRoutine());

    }
    
    //show the updates in the UI hearts
    private void UpdateHealthUI()
     {
         for (int i = 0; i < healthHearts.Length; i++) // we need to loop through each heart in the array
    
         {
             if (i < _currentHealth)
             {
                 healthHearts[i].sprite = fullHeart;
             }
             else
             {
                 healthHearts[i].sprite = emptyHeart;
             }
         }
     }
    
    private IEnumerator InvincibilityCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityDuration);
        _canTakeDamage = true;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator RespawnRoutine()
    {
        
        yield return new WaitForSeconds(respawnDelay);
        
        transform.position = respawnPoint.position; // this will move the player to the point
        _currentHealth = maxHealth; // reset health
        UpdateHealthUI();
        _isDead = false; // reset death
        
        StartCoroutine(InvincibilityCooldown());

    }
    
}
