using UnityEngine;

/// <summary>
/// The purpose of this script is to handle the player interactions.
/// These interactions are based on the trigger collider2D that is a child object to the Player
/// </summary>
public class PlayerInteractions : MonoBehaviour
{
    public GameManager GameManager; // a reference to the GameManager

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            //TODO
            // Get the SpawnPoint.position from the DoorInteraction script (on the door the player collided with)
            // Set the CurrentSpawnPoint from the GameManager equal to the Door's SpawnPoint.position
            // Set CanEnterDoor from the GameManager to true
            GameManager.CurrentSpawnPoint = collision.GetComponent<DoorInteraction>().SpawnPoint.position;
            GameManager.CanEnterDoor = true;
        }

        if (collision.CompareTag("Treasure"))
        {
            //TODO
            // Set CanOpenItem from the GameManager to true
            // Get the TreasureInteraction component from the Treasure object the player collided with
            // Set the CurrentTreasureInteraction equal to the TreasureInteraction component
            GameManager.CanOpenItem = true;
            GameManager.CurrentTreasureInteraction = collision.GetComponent<TreasureInteraction>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            //TODO
            // Set CanEnterDoor from the GameManager to false
            GameManager.CanEnterDoor = false;
        }
        
        if (collision.CompareTag("Treasure"))
        {
            //TODO
            // Set CanOpenItem from the GameManager to false
            GameManager.CanOpenItem = false;
        }
    }
}
