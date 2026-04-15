using UnityEngine;

/// <summary>
/// Used to move the dragon to the center of the screen
/// </summary>
public class DragonMover : MonoBehaviour
{
    /// <summary>
    /// Used to represent that a distance value is empty
    /// </summary>
    private const int NO_DISTANCE = 0;

    /// <summary>
    /// The Transform for the Dragon object and all its children
    /// </summary>
    [SerializeField]
    private Transform _dragon;
    /// <summary>
    /// The position the Dragon is moving to
    /// </summary>
    private Vector3 _toPosition;
    /// <summary>
    /// The total distance the Dragon has travelled on its path to the _toPosition
    /// </summary>
    private float _distanceTravelled;
    /// <summary>
    /// The total distance needed for the Dragon to travel from its starting position to it _toPosition
    /// </summary>
    private float _movementDistance;
    /// <summary>
    /// The Dragon's movement speed along its path. 0.2 was chosen as default, because I thought it felt best.
    /// </summary>
    [SerializeField]
    private float _moveSpeed = 0.2f;

    /// <summary>
    /// Event that triggers when the movement is finished
    /// </summary>
    public UnityEngine.Events.UnityEvent FinishedMoveEvent;

    void FixedUpdate()
    {
        // Moves the Dragon once per frame
        Move();
        // If the Dragon has reached its destination, runs FinishedMove() to signal that the journey has been completed
        if (_dragon.position == _toPosition)
        {
            FinishedMove();
        }
    }

    /// <summary>
    /// Starts the Dragon's movement towards the point _moveTo
    /// </summary>
    /// <param name="_moveTo">The position that the Dragon is moving to.</param>
    public void StartMovement(Vector3 _moveTo)
    {
        // Sets the _toPosition to the _moveTo position
        _toPosition = _moveTo;
        Debug.Log($"DragonMover is starting movement to position {_toPosition} from {_dragon.position}");
        // Calculates the total distance that the movement path will take
        _movementDistance = Vector3.Distance(_dragon.position, _toPosition);
    }

    /// <summary>
    /// Moves the Dragon along its movement path to _toPosition
    /// </summary>
    private void Move()
    {
        // If the _movement distance is an actual amount that can be travelled
        if (_movementDistance > NO_DISTANCE)
        {
            //Debug.Log($"Dragon Moving at Position: {_dragon.position} To: {{_toPosition.position}}\"");

            // Adds _moveSpeed amount of distance to how far along the Dragon has travelled along its path
            _distanceTravelled += _moveSpeed;

            // The percentage of the total distance that the next movement will represent
            float distancePercent = _distanceTravelled / _movementDistance;

            // Uses lerp between the to and Dragon's current position to place the Dragon a distancePercent between the two points
            _dragon.position = Vector3.Lerp(_dragon.position, _toPosition, distancePercent);

            // Debug.Log($"Percent: {distancePercent}, Distance: {_movementDistance}");
        }
        // 
        else
        {
            // If the Dragon is attempting to not move, log error
            if (_movementDistance == NO_DISTANCE)
            {
                Debug.LogError($"ERROR: DragonMover is trying to move a distance of 0");
            }
            // If the Dragon is attempting to move a negative amount, log error
            else
            {
                Debug.LogError($"ERROR: DragonMover is trying to move a negative distance");
            }
        }
    }

    /// <summary>
    /// Used to tell any relevant parties that the Dragon has finished its movement
    /// </summary>
    private void FinishedMove()
    {
        FinishedMoveEvent?.Invoke();
        // Disables the Mover since it has fulfilled its purpose
        this.enabled = false;
    }
}
