using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    
    //INPUT ACTIONS
    private MainActions _testActions; // this _testActions is now the new name of PlayerInputActions || underscores are used when the value is private
    public System.Action OnJump;
    public System.Action <float> OnMove; // this action needs a value inside <> -- that is why we add float.
    
    
    private void Awake()
    {
        _testActions = new MainActions(); // create the input actions object
        _testActions.Enable(); // enable when the component is enabled
        Debug.Log("Main Actions created");
    }

    void OnEnable()
    { 
        _testActions.Character.Jump.performed += OnJumpPressed; // when jump is performed, call the Jump function
        //_testActions.Character.Move.performed += OnMovement;

    }

    void OnDisable()
    {
            _testActions.Character.Jump.performed -= OnJumpPressed;
           //_testActions.Character.Move.performed -= OnMovement;
        
    }

    void OnJumpPressed(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(); // "if the jump has listeners then invoke the jump action - lsiteners are the input system
        Debug.Log ("Jumped!");
    }

    void OnMovement()
    {
        //_moveInput = ctx.ReadValue<Vector2>();
        OnMove?.Invoke(_testActions.Character.Move.ReadValue<float>());
        //Debug.Log("Move Input: " + ctx.ReadValue<float>());
    }

    private void Update()
    {
        OnMovement();
    }
    
    
}
