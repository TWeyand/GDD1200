using UnityEngine;
using TMPro;

/// <summary>
/// Controls the UI being displayed during the game
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// The current display that is being shown
    /// </summary>
    private GameObject _activeDisplay;
    /// <summary>
    /// The display of the keypad
    /// </summary>
    [SerializeField]
    private GameObject _keypadDisplay;
    /// <summary>
    /// The display of the dragon guessing phase
    /// </summary>
    [SerializeField]
    private GameObject _guessingDisplay;
    /// <summary>
    /// The text object inside of the _guessingDisplay that contains the dragon's guess
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _guessText;
    /// <summary>
    /// The display for when the round is ended
    /// </summary>
    [SerializeField]
    private GameObject _endroundDisplay;
    /// <summary>
    /// The display for when the player lies
    /// </summary>
    [SerializeField]
    private GameObject _liarDisplay;
    /// <summary>
    /// The display for when the player dies
    /// </summary>
    [SerializeField]
    private GameObject _deathDisplay;

    private void Start()
    {
        // Subscribes to necessary events
        GameManager.instance.MyEvents.StartPlayerNumberPhaseEvent.AddListener(OnStartPlayerNumberPhase);
        GameManager.instance.MyEvents.StartGuessingPhaseEvent.AddListener(OnStartGuessingPhase);
        GameManager.instance.MyEvents.SetGuessEvent.AddListener(OnSetGuess);
        GameManager.instance.MyEvents.EndRoundEvent.AddListener(OnEndRound);
        GameManager.instance.MyEvents.PlayerLiedEvent.AddListener(OnPlayerLied);
        GameManager.instance.MyEvents.RetractLieEvent.AddListener(OnRetractLie);
        GameManager.instance.MyEvents.LieDoubleDownEvent.AddListener(OnLieDoubleDown);
        GameManager.instance.MyEvents.PlayerDeathEvent.AddListener(OnPlayerDeath);
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
        GameManager.instance.MyEvents.StartGuessingPhaseEvent.RemoveListener(OnStartGuessingPhase);
        GameManager.instance.MyEvents.SetGuessEvent.RemoveListener(OnSetGuess);
        GameManager.instance.MyEvents.EndRoundEvent.RemoveListener(OnEndRound);
        GameManager.instance.MyEvents.PlayerLiedEvent.RemoveListener(OnPlayerLied);
        GameManager.instance.MyEvents.RetractLieEvent.RemoveListener(OnRetractLie);
        GameManager.instance.MyEvents.LieDoubleDownEvent.RemoveListener(OnLieDoubleDown);
        GameManager.instance.MyEvents.PlayerDeathEvent.RemoveListener(OnPlayerDeath);
    }

    /// <summary>
    /// Sets the activeDisplay that is being shown to the player
    /// </summary>
    public void SetActiveDisplay(GameObject canvasObject)
    {
        // If the canvasObject display is not currently being shown
        if (canvasObject != _activeDisplay)
        {
            // Logs which display is being set as active
            Debug.Log($"Setting {canvasObject} as the active display");

            // Disables the currently active display if there is already an active display
            if (_activeDisplay != null) _activeDisplay.SetActive(false);

            // Sets the canvasObject display as the new _activeDisplay
            _activeDisplay = canvasObject;
            // Sets the new _activeDisplay to active so that it can be seen and interacted with
            _activeDisplay.SetActive(true);
        }
    }

    /// <summary>
    /// Disables the active display and sets the new active display to null
    /// </summary>
    public void ClearActiveDisplay()
    {
        // Disables the active display if there is an active display
        if (_activeDisplay != null) _activeDisplay.SetActive(false);
        _activeDisplay = null;
    }


    #region Event Receivers
    /// <summary>
    /// Displays the keypad as the active display for the PlayerNumberPhase so they can enter a number
    /// </summary>
    private void OnStartPlayerNumberPhase()
    {
        SetActiveDisplay(_keypadDisplay);
    }

    /// <summary>
    /// Displays the _guessingDisplay as the active display for the GuessingPhase so they can give the dragon feedback on its guess.
    /// </summary>
    public void OnStartGuessingPhase()
    {
        SetActiveDisplay(_guessingDisplay);
    }

    /// <summary>
    /// Sets the _guessText of the _guessingDisplay to the Dragon's current guess when the Dragon makes a new guess.
    /// </summary>
    /// <param name="guess">The Dragon's current guess.</param>
    public void OnSetGuess(int guess)
    {
        _guessText.text = guess.ToString();
    }

    /// <summary>
    /// Displays the _endroundDisplay as the active display for the EndRound phase
    /// </summary>
    public void OnEndRound()
    {
        SetActiveDisplay(_endroundDisplay);
    }

    /// <summary>
    /// Displays the _liarDisplay as the active display fwhen the player has lied
    /// </summary>
    public void OnPlayerLied()
    {
        SetActiveDisplay(_liarDisplay);
    }

    /// <summary>
    /// Displays the _guessingDisplay as the active display when the player has retracts a lie so they can go back to the GuessingPhase
    /// </summary>
    public void OnRetractLie()
    {
        SetActiveDisplay(_guessingDisplay);
    }

    /// <summary>
    /// Clears all displays when the player doubles down. This is so nothing gets in the way of the dragon killing the player
    /// </summary>
    public void OnLieDoubleDown()
    {
        ClearActiveDisplay();
    }

    /// <summary>
    /// Displays the _deathDisplay when the player dies so that they can return to main menu or quit the game
    /// </summary>
    public void OnPlayerDeath()
    {
        SetActiveDisplay(_deathDisplay);
    }

    #endregion
}
