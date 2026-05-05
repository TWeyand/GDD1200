using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

/// <summary>
/// The purpose of this script is to put the managers into a single location and control general game stuff
/// </summary>
public class GameManager : MonoBehaviour
{
    public AudioManager AudioManager; // reference to the AudioManager script 
    public UIManager UIManager;  // reference to the UIManager script
    public AnimationManager AnimationManager;  // reference to the AnimationManager script

    public GameObject Player; // reference to the player gameObject
    
    // The following are hidden in the inspector
    [HideInInspector] public Vector2 CurrentSpawnPoint; // the current spawn point to teleport the player on respawn
    [HideInInspector] public TreasureInteraction CurrentTreasureInteraction; // the current treasure element the player is interacting with
    [HideInInspector] public bool CanEnterDoor; // bool for checking if the player can enter through a door
    [HideInInspector] public bool CanOpenItem; // bool for checking if the player can open an item

    private PlayerInputManager _playerInput; // reference to the PlayerInputManager script

    private void Start()
    {
        //TODO
        // Cache the reference to the PlayerInputManager script into the _playerInput variable
        // Hint: you can get it directly from the Player gameObject
        _playerInput = Player.GetComponent<PlayerInputManager>();
    }

    private void Update()
    {
        //TODO
        // Check if the player's movement is not equal to (0,0)
        // If that is the case, then call PlayFootstepsSFX from the AudioManager
        // Hint: GetMoveInput from the PlayerInput class will return the current value of moveInput
        if (_playerInput.GetMoveInput() != Vector2.zero)
        {
            AudioManager.PlayFootstepsSFX();
        }
    }
    
    public void GoThroughDoor()
    {
        // TODO:
        // Call the TeleportSequence Coroutine
        StartCoroutine(TeleportSequence());
    }
    
    public void OpenItem()
    {
        if (!CurrentTreasureInteraction.HasOpenedChest)
        {
            // TODO:
            // Call the PlayOpenChestSFX() function from the AudioManager
            AudioManager.PlayOpenChestSFX();
            // Call the OpenPanel() function from the UIManager
            UIManager.OpenPanel();
            // Set HasOpenedChest from the CurrentTreasureInteraction to true
            CurrentTreasureInteraction.HasOpenedChest = true;
            // Set InteractionUI from the CurrentTreasureInteraction to false
            CurrentTreasureInteraction.InteractionUI.SetActive(false);
        }
    }

    public void EnablePlayerInput(bool value)
    {
        // TODO:
        // Set _playerInput.enabled to equal the value we pass through
        _playerInput.enabled = value;
    }
    
    
    private IEnumerator TeleportSequence()
    {
        //TODO
        // Set the Player Input to false using EnablePlayerInput() function
        EnablePlayerInput(false);
        // Call PlayTeleportOutSFX() from the AudioManager
        AudioManager.PlayTeleportOutSFX();
        // Set the Teleporting animation bool to true from the Animation Manager
        AnimationManager.SetTeleportingAnimationBool(true);
        // Wait for .5 seconds
        yield return new WaitForSeconds(0.5f);
        // set the Player's transform.position equal to the CurrentSpawnPoint
        Player.transform.position = CurrentSpawnPoint;
        // Wait for .1 seconds
        yield return new WaitForSeconds(0.1f);
        // Call PlayTeleportInSFX() from the AudioManager
        AudioManager.PlayTeleportInSFX();
        // Set the Teleporting animation bool to false from the Animation Manager
        AnimationManager.SetTeleportingAnimationBool(false);
        // Wait for .5 seconds
        yield return new WaitForSeconds(0.5f);
        // Set the Player Input to true using EnablePlayerInput() function
        EnablePlayerInput(true);
        yield return null;
    }
}
