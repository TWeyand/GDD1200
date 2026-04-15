using UnityEngine;

public class HudManager : MonoBehaviour
{
    /// <summary>
    /// Integer that represents that no attempts have been made and that no number has been set
    /// </summary>
    private const int EMPTY_TEXT = 0;

    [SerializeField]
    private GameObject _hudElements;
    [SerializeField]
    private TMPro.TextMeshProUGUI _playerNumberText;
    [SerializeField]
    private TMPro.TextMeshProUGUI _attemptsText;

    /// <summary>
    /// Subsribes to required events when awoken
    /// </summary>
    private void Awake()
    {
        GameManager.instance.MyEvents.UpdateAttemptsEvent.AddListener(OnUpdateAttempts);
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.AddListener(OnValidPlayerNumber);
        GameManager.instance.MyEvents.StartPlayerNumberPhaseEvent.AddListener(OnStartPlayerNumberPhase);
        GameManager.instance.MyEvents.LieDoubleDownEvent.AddListener(OnLieDoubleDown);
        GameManager.instance.MyEvents.EndRoundEvent.AddListener(OnEndRound);
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
        GameManager.instance.MyEvents.UpdateAttemptsEvent.RemoveListener(OnUpdateAttempts);
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.RemoveListener(OnValidPlayerNumber);
        GameManager.instance.MyEvents.StartPlayerNumberPhaseEvent.RemoveListener(OnStartPlayerNumberPhase);
        GameManager.instance.MyEvents.LieDoubleDownEvent.RemoveListener(OnLieDoubleDown);
        GameManager.instance.MyEvents.EndRoundEvent.RemoveListener(OnEndRound);
    }

    /// <summary>
    /// Sets the playerNumber text in the hud to display the argument playerNumber.
    ///     The Player Number exists on the hud so that the player does not forget their number
    /// </summary>
    /// <param name="playerNumber"></param>
    private void SetNumberText(int playerNumber = EMPTY_TEXT)
    {
        // Uses string interpolation to convert the playerNumber int to a string
        _playerNumberText.text = $"Your Number: {playerNumber}";
    }

    /// <summary>
    /// Sets the text that represents the total number of guesses the dragon has made so far
    ///     
    /// </summary>
    private void SetAttemptsText(int attempts = EMPTY_TEXT)
    {
        // Uses string interpolation to convert the attempts int to a string
        _attemptsText.text = $"Total Guesses: {attempts}";
    }

    /// <summary>
    /// Erases the text on the Hud so that it can be reset whenever needed
    /// </summary>
    private void ClearText()
    {
        _playerNumberText.text = "";
        _attemptsText.text = "";
    }

    #region Event Receivers

    /// <summary>
    /// Clears the text at the start of a round so that it doesn't contain any residual data from a previous round.
    /// </summary>
    private void OnStartPlayerNumberPhase()
    {
        ClearText();
    }

    /// <summary>
    /// Updates the total attempts display whenever the dragon makes a new guess
    /// </summary>
    /// <param name="attempts">The current amount of attempts that needs to be displayed</param>
    private void OnUpdateAttempts(int attempts)
    {
        SetAttemptsText(attempts);
    }

    /// <summary>
    /// Once a valid playerNumber has been entered, sets the playerNumber to the hud in case the player forgets the number they chose
    /// </summary>
    /// <param name="playerNumber">The number that the player selected</param>
    private void OnValidPlayerNumber(int playerNumber)
    {
        SetNumberText(playerNumber);
        SetAttemptsText();
    }

    /// <summary>
    /// When the player doubles down on a lie, Deactivates the hud elements
    ///     Does this so that the hud is not in the way of the death screen
    /// </summary>
    private void OnLieDoubleDown()
    {
        ClearText();
        //_hudElements.SetActive(false);
    }

    /// <summary>
    /// Clears the text on the HUD when the round has ended. This is done for aesthetic reasons
    /// </summary>
    private void OnEndRound()
    {
        ClearText();
    }

    #endregion
}
