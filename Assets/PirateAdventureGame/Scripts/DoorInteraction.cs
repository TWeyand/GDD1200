using UnityEngine;

/// <summary>
/// The purpose of this script is to handle the Player's interactions with doors
/// </summary>
public class DoorInteraction : MonoBehaviour
{
    public Transform SpawnPoint; // a reference to the spawnpoint for this door (where the player will teleport)
    public GameObject InteractionUI; // a reference to the Interaction UI

    private void Start()
    {
        //TODO
        // Set the visibility of the InteractionUI to false
        InteractionUI.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO
            // Set the visibility of the InteractionUI to true
            InteractionUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //TODO
            // Set the visibility of the InteractionUI to false
            InteractionUI.SetActive(false);
        }
    }
}
