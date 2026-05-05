using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// The purpose of this script is to handle all of the Player's input actions
/// </summary>
public class PlayerInputManager : MonoBehaviour
{
    public GameManager GameManager;

    private PlayerControls _input;
    private Vector2 _moveInput;

    private void Awake()
    {
        //TODO
        // Create a new instance of the PlayerControls object and store it in the _input variable
        _input = new PlayerControls();
        // Subscribe from the Move.performed input to the OnMovePerformed reference
        _input.Player.Move.performed += OnMovePerformed;
        // Subscribe from the Move.canceled input to the OnMoveCanceled reference
        _input.Player.Move.canceled += OnMoveCanceled;
        // Subscribe from the Interact.performed input to the OnInteractPerformed reference
        _input.Player.Interact.performed += OnInteractPerformed;
    }
    
    // Enable & Disable the Player Action
    private void OnEnable()
    {
        //TODO
        // Enable the Player Action Map
        _input.Player.Enable();
    }

    private void OnDisable()
    {
        //TODO
        // Disable the Player Action Map
        _input.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        //TODO
        // Set _moveInput to equal the context read as a Vector2
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        //TODO
        // Set _moveInput to equal (0,0)
        _moveInput = Vector2.zero;
    }
    
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (GameManager.CanEnterDoor)
        {
            //TODO
            // Call GoThroughDoor() from the GameManager
            GameManager.GoThroughDoor();
        }
        
        else if (GameManager.CanOpenItem)
        {
            //TODO
            // Call OpenItem() from the GameManager
            GameManager.OpenItem();
        }
    }

    // Getter Functions
    public Vector2 GetMoveInput()
    {
        return _moveInput;
    }
}
