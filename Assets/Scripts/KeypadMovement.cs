using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

/// <summary>
/// Used to move the Keypad on and off screen
/// </summary>
public class KeypadMovement : MonoBehaviour
{
    /// <summary>
    /// Used to represent that the keypad has not travelled at all along its current path yet.
    /// </summary>
    private const int NO_TRAVELLED_DISTANCE = 0;
    
    /// <summary>
    /// The states of where/what the keypad is currently at/doing
    /// </summary>
    enum States
    {
        Offscreen,  // At the Offscreen Position
        MoveIn,     // Moving into the Onscreen Position
        Onscreen,   // At the Onscreen Position
        MoveOut,    // Moving into the Offscreen Position
    }

    /// <summary>
    /// The current state of the keypads location/movement.
    /// Starts at offscreen since the keypad starts offscreen
    /// </summary>
    [SerializeField]
    private States state = States.Offscreen;

    /// <summary>
    /// The transform of the keypad
    /// </summary>
    [SerializeField]
    private RectTransform _keypadTransform;

    /// <summary>
    /// The point that the keypad will move to in order to be seen onscreen
    /// </summary>
    [SerializeField]
    private RectTransform OnScreenPoint;
    /// <summary>
    /// The point that the keypad will move to in order to <b>NOT</b> be seen onscreen
    /// </summary>
    [SerializeField]
    private RectTransform OffScreenPoint;

    /// <summary>
    /// The point the keypad is moving from
    /// </summary>
    private Vector3 _fromPosition;
    /// <summary>
    /// The point that the keypad is trying to move to
    /// </summary>
    private Vector3 _toPosition;
    /// <summary>
    /// The speed that the keypad moves onscreen/offscreen.
    /// 50.0f is selected as the default as it felt best.
    /// </summary>
    [SerializeField]
    private float _moveSpeed = 50.0f;
    /// <summary>
    /// The total distance the keypad needs to move over the course of its whole journey.
    /// Used to track the current distance that has been travelled compared to the total distance needed to be travelled.
    /// </summary>
    private float _movementDistance;
    /// <summary>
    /// The total distance that the keypad has currently moved on its journey.
    /// Used to track the current distance that has been travelled compared to the total distance needed to be travelled.
    /// </summary>
    private float _distanceTravelled;

    /// <summary>
    /// Subscribes to necessary events
    /// </summary>
    private void Awake()
    {
        GameManager.instance.MyEvents.StartPlayerNumberPhaseEvent.AddListener(OnStartPlayerNumberPhase);
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.AddListener(OnValidPlayerNumber);
    }

    // Clears event subscriptions when object is destroyed
    private void OnDestroy()
    {
        /* Attempts to Unsubscribe from all events
         *      This will not cause any issues even if the MyEvents object is already deleted
         *      so checking for that is unneeded */
        UnsubscribeFromMyEvents();
    }

    /// <summary>
    /// Unsubscribes from all events connected to the MyEvents object.
    /// Unsubscribing from events is important to protect memory leaks.
    /// </summary>
    private void UnsubscribeFromMyEvents()
    {
        GameManager.instance.MyEvents.StartPlayerNumberPhaseEvent.RemoveListener(OnStartPlayerNumberPhase);
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.RemoveListener(OnValidPlayerNumber);
    }
    
    private void FixedUpdate()
    {
        // Moves each FixedUpdate when in a movement state 
        switch(state)
        {
            // When the keypad is moving onto the screen
            case States.MoveIn:
                // Moves the keypad
                Move();
                // If the keypad has reached or passed its onscreen destination, moves to the onscreen state
                if (_keypadTransform.position.x <= _toPosition.x)
                {
                    // Ensures that the keypad is actually at the _toPosition
                    _keypadTransform.position = _toPosition;
                    ChangeState(States.Onscreen);
                }
                break;

            // When the keypad is moving off of the screen
            case States.MoveOut:
                // Moves the keypad
                Move();
                // If the keypad has reached or passed its offscreen destination, moves to the offscreen state
                if (_keypadTransform.position.x >= _toPosition.x)
                {
                    _keypadTransform.position = _toPosition;
                    ChangeState(States.Offscreen);
                }
                break;
        }
    }

    /// <summary>
    /// Starts the keypad's movement
    /// </summary>
    public void StartMovement()
    {
        Debug.Log("Keypad Starting Movement");
        // Gets the current point that the keypad is at and sets that as the position that is being moved from
        _fromPosition = _keypadTransform.position;
        switch (state)
        {
            // If the keypad is currently offscreen, sets the point its moving towards to the OnScreenPoint,
            //  and changes the state to MoveIn so that the keypads movement can begin onscreen
            case States.Offscreen:
                _toPosition = OnScreenPoint.position;
                ChangeState(States.MoveIn);
                break;
            // If the keypad is currently onscreen, sets the point its moving towards to the OffScreenPoint,
            //  and changes the state to MoveOut so that the keypads movement can begin offscreen
            case States.Onscreen:
                _toPosition = OffScreenPoint.position;
                ChangeState(States.MoveOut);
                break;
            // In case the player somehow manages to enter in the keypad number before the keypad finished moving onscreen
            //  sets the point its moving towards to the OffScreenPoint,
            //  and changes the state to MoveOut so that the keypads movement can begin moving offscreen
            case States.MoveIn:
                _toPosition = OffScreenPoint.position;
                ChangeState(States.MoveOut);
                break;
            // There should never be a situation where the keypad is attempting a new movement while it is already moving offscreen
            //  This is mostly due to the fact that the keypad's buttons are disabled when it is moving offscreen,
            //  so it cannot get any new input to change what its doing
            default:
                break;
            

        }
        // Sets that the keypad has not moved at all yet
        _distanceTravelled = NO_TRAVELLED_DISTANCE;
        // Gets the distance between the from and to positions in order to find out how long the journey will be
        _movementDistance = Vector3.Distance(_fromPosition, _toPosition);
    }

    /// <summary>
    /// Moves the keypad by _moveSpeed along its travel path
    /// </summary>
    private void Move()
    {
        // Logs where keypad is on its journey
        // Debug.Log($"Keypad Moving at Position: {_keypadTransform.position}");

        // Adds _moveSpeed amount of distance to how far along the keypad has travelled along its journey
        _distanceTravelled += _moveSpeed;

        // The percentage of the total distance that the next movement will represent
        float distancePercent = _distanceTravelled / _movementDistance;
        
        // Uses lerp between the to and from positions to place the keypad a distancePercent between the two points
        _keypadTransform.position = Vector3.Lerp(_fromPosition, _toPosition, distancePercent);
        // Uses Mathf.Round() on position coordinates in order to combat rounding issues.
        _keypadTransform.position = new Vector3(Mathf.Round(_keypadTransform.position.x), Mathf.Round(_keypadTransform.position.y), Mathf.Round((int)_keypadTransform.position.z));

        // Logs information about the journey to the console
        // Debug.Log($"To: {_toPosition}, From: {_fromPosition}");
        // Debug.Log($"Percent: {distancePercent}, Distance: {_movementDistance}");
    }

    /// <summary>
    /// Changes the state of the KeypadMovement object to the newState
    /// </summary>
    /// <param name="newState">The state being swapped to.</param>
    private void ChangeState(States newState)
    {
        // Assigns the state to the newState
        state = newState;

        /* If the keypad is being switched to the offscreen state, then it must have been moved offscreen after the PlayerNumberPhase has ended.
         * Therefore, tells the GameManager to end the PlayerNumberPhase when the keypad is switched to the Offscreen phase.
         *  This happens because the keypad needs to be offscreen before the GuessingPhase's display can be turned off at the end of the phase.
         *  If the keypad was turned off before than, it would awkwardly blink out of existance when the display changes.
         *  As such, the PlayerNumberPhase can only end once the keypad has successfully reached the offscreen position. */
        if(state == States.Offscreen) {
            GameManager.instance.MyEvents.EndPlayerNumberPhase();
        }
    }

    /// <summary>
    /// When the PlayerNumberPhase starts, starts to move the keypad so it can head onscreen to receive the player's input
    /// </summary>
    public void OnStartPlayerNumberPhase()
    {
        StartMovement();
    }

    /// <summary>
    /// When the player has submitted a valid number, starts to move the keypad so it can head offscreen since it is no longer needed.
    /// </summary>
    public void OnValidPlayerNumber(int playerNumber)
    {
        StartMovement();
    }
}
