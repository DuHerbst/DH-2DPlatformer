using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour

{
    private MainActions _testActions; // this _testActions is now the new name of PlayerInputActions

    private void Awake()
    {
        _testActions = new MainActions(); // we have created an object now!! this is the name of the inputs created at first and then we say taht we willc reate a NEW one
        _testActions.Enable(); // we turn it on to listen to key inputs
        
    }

    void OnEnable()
    {
        _testActions.Character.Jump.performed -= Jump; // when jump is performed, call the Jump function
        
    }

    void OnDisable()
    {
        _testActions.Character.Jump.performed -= Jump;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jumped!");
    }
    
    
}
