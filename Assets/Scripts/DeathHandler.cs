using UnityEngine;

/// <summary>
/// Manages the steps needed to be taken before the Dragon kills the player
/// </summary>
[RequireComponent (typeof(DragonMover))]
public class DeathHandler : MonoBehaviour
{
    private DragonMover _dragonMover;
    /// <summary>
    /// The position of the dragon on the death screen
    /// </summary>
    private Vector3 _dragonKillPosition;

    void Awake()
    {
        _dragonMover = GetComponent<DragonMover>();

        // Ensures the Dragon mover is disabled so it does not try to move the dragon yet
        if (_dragonMover.enabled == true)
        {
            _dragonMover.enabled = false;
        }

        // Sets the postion that the Dragon kills you at to center with the camera.
        //  Ignores the Z position so that the Dragon sprite doesn't move behind the background 
        _dragonKillPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y);

        // Listens for when the player doubles down as that is when the player dies
        GameManager.instance.MyEvents.LieDoubleDownEvent.AddListener(OnLieDoubleDown);
    }

    // Clears event subscriptions when object is destroyed
    private void OnDestroy()
    {
        /* Attempts to Unsubscribe from all events
         *      This will not cause any issues even if the MyEvents object is already deleted
         *      so checking for that is unneeded */
        UnsubscribeFromMyEvents();
    }

    private void UnsubscribeFromMyEvents()
    {
        GameManager.instance.MyEvents.LieDoubleDownEvent.RemoveListener(OnLieDoubleDown);
    }

    // Enables the DragonMover and tells it to move the Dragon to the kill position
    private void MoveDragon()
    {
        Debug.Log("DeathHandler is attempting to move the dragon");
        
        // Enables Dragon Mover so it starts moving the Dragon
        _dragonMover.enabled = true;
        // Listens to DragonMover's FinishedMoveEvent so the DeathHandler knows when the Dragon has reached the kill position
        _dragonMover.FinishedMoveEvent.AddListener(OnFinishedMove);
        // Starts moving the Dragon to the kill position
        _dragonMover.StartMovement(_dragonKillPosition);
    }

    /// <summary>
    /// Starts moving the Dragon once the player has doubled down on their lie.
    /// </summary>
    private void OnLieDoubleDown()
    {
        MoveDragon();
    }

    /// <summary>
    /// Updates the Dragon animator to begin the steps to burning the player once the Dragon has reached the kill position.
    /// </summary>
    private void OnFinishedMove()
    {
        Debug.Log("Dragon Finished Moving");
        // Unsubs from FinishedMoveEvent as it is no longer necessary
        _dragonMover.FinishedMoveEvent.RemoveListener(OnFinishedMove);
        // Tells the GameManager to Update the Dragon's mood to the fire breath mood
        GameManager.instance.MyEvents.UpdateDragonMood(AnimationStateMachine.MoodLevel.Fire);
    }
}
