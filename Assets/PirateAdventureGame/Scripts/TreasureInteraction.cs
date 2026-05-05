using UnityEngine;

/// <summary>
/// The purpose of this script is to handle Player interactions with the treasure objects
/// </summary>
public class TreasureInteraction : MonoBehaviour
{
    public GameObject InteractionUI; // a reference to the Interaction UI
    public bool HasOpenedChest = false; // the bool check that prevents Players from opening an already opened chest

    private void Start()
    {
        //TODO
        // Set the visibiliy of the InteractionUI to false
        InteractionUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !HasOpenedChest)
        {
            //TODO
            // Set the visibility of the InteractionUI to true
            InteractionUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || HasOpenedChest)
        {
            //TODO
            // Set the visibility of the InteractionUI to false
            InteractionUI.SetActive(false);
        }
    }
    
}
