using UnityEngine;

/// <summary>
/// The purpose of this script is the handle moving the player based on the player input
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 5.0f; // the movement speed for the player
    
    private Rigidbody2D _rigidbody2d; // a reference to the rigidbody2D
    private PlayerInputManager _inputManager; // a reference to the PlayerInputManager

    private void Start()
    {
        //TODO
        // cache the reference of the Rigidbody2D component
        // cache the reference of the PlayerInputManager
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _inputManager = GetComponent<PlayerInputManager>();
    }

    private void FixedUpdate()
    {
        //TODO
        // Call the DoMove() function 
        DoMove();
    }

    private void DoMove()
    {
        //TODO
        // Set the linearVelocity on the rigidbody to equal the moveInput (from the _inputManager) times _moveSpeed
        _rigidbody2d.linearVelocity = _inputManager.GetMoveInput() * _moveSpeed;
    }
}
