using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    //INPUT ACTIONS
    private MainActions playerActions; // this playerActions is now the new name of PlayerInputActions || underscores are used when the value is private
    public System.Action OnJump;
    public System.Action <float> Move; // this action needs a value inside <> -- that is why we add float.
    public System.Action<float> Vertical;


    private void Awake()
    {
        playerActions = new MainActions(); // create the input actions object
        playerActions.Enable(); // enable when the component is enabled
        Debug.Log("Main Actions created"); 
    }

    void OnEnable()
    {
        playerActions.Character.Jump.performed += OnJumpPressed; // when jump is performed, call the Jump function
        //_testActions.Character.Move.performed += OnMovement;
    
    }
    
    void OnDisable()
    {
        playerActions.Character.Jump.performed -= OnJumpPressed;
           //_testActions.Character.Move.performed -= OnMovement;
    
    }

    void OnJumpPressed(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke(); // if the jump has listeners then invoke the jump action - lsiteners are the input system
    }
    
    void OnMovement()
    {
        Move?.Invoke(playerActions.Character.Horizontal.ReadValue<float>());
        Vertical?.Invoke(playerActions.Character.Vertical.ReadValue<float>());
    }
    
    private void Update()
    {
        OnMovement();
    }


}
