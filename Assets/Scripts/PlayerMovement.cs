using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    // FEATURES
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float accelerationRampUp = 1f;
    
    //INPUT ACTIONS
    
    private MainActions _testActions; // this _testActions is now the new name of PlayerInputActions || underscores are used when the value is private
    private Vector2 _moveInput;
    public System.Action onJump;
    public System.Action <float> onMove; // this action needs a value inside <> -- that is why we add float.
    
    
    private void Awake()
    {
        _testActions = new MainActions(); // create the input actions object
        _testActions.Enable(); // enable when the component is enabled
        Debug.Log("Main Actions created");
    }

    void OnEnable()
    { 
        _testActions.Character.Jump.performed += OnJumpPressed; // when jump is performed, call the Jump function
        _testActions.Character.Move.performed += OnMove;

    }

    void OnDisable()
    {
            _testActions.Character.Jump.performed -= OnJumpPressed;
            _testActions.Character.Move.performed -= OnMove;
        
    }

    void OnJumpPressed(InputAction.CallbackContext ctx)
    {
        onJump?.Invoke(); // "if the jump has listeners then invoke the jump action - lsiteners are the input system
        Debug.Log ("Jumped!");
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        //_moveInput = ctx.ReadValue<Vector2>();
        onMove?.Invoke(ctx.ReadValue<float>()); // The value of invoke is taken from the Read Value. Invokes the move action
        Debug.Log("Move Input: " + ctx.ReadValue<float>());
    }

    private void Update()
    {
        Vector3 move = new Vector3(_moveInput.x,_moveInput.y, 0);
        transform.Translate(move * (moveSpeed * Time.fixedDeltaTime));
    }
    
    
}
