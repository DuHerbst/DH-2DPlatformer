using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    // FEATURES
    [SerializeField] private float moveSpeed = 5f;
    
    private MainActions _testActions; // this _testActions is now the new name of PlayerInputActions
    private Vector2 _moveInput;
    
    private void Awake()
    {
        _testActions = new MainActions(); // create the input actions object
        Debug.Log("PlayerMovement Awake: MainActions created");
    }

    void OnEnable()
    { 
        _testActions = new MainActions();
        _testActions.Enable(); // enable when the component is enabled

        _testActions.Character.Jump.performed += Jump; // when jump is performed, call the Jump function
        _testActions.Character.Move.performed += OnMove;
        _testActions.Character.Move.canceled += OnMove;

    }

    void OnDisable()
    {
            _testActions.Character.Jump.performed -= Jump;
            _testActions.Character.Move.performed -= OnMove;
            _testActions.Character.Move.canceled -= OnMove;
            _testActions.Disable();
            
            Debug.Log("PlayerMovement OnDisable: actions disabled");
        
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log ("Jumped!");
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        Debug.Log("Move Input: " + _moveInput + " | phase: " + ctx.phase);
    }

    private void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.Translate(move * (moveSpeed * Time.fixedDeltaTime));
    }
    
    
}
