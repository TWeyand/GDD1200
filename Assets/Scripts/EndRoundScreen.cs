using UnityEngine;
using TMPro;

public class EndRoundScreen : MonoBehaviour
{
    /// <summary>
    /// The text object that displays what the player's guess was
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _yourNumberText;
    /// <summary>
    /// The text object that displays how many attempts it took the Dragon to guess the player's number
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _attemptsText;

    [SerializeField]
    private GameObject _winElements;
    [SerializeField]
    private GameObject _loseElements;

    /// <summary>
    /// Stores the total amount of guess attempts the Dragon has made.
    /// </summary>
    private int _dragonAttempts = 0;

    public void Awake()
    {
        // Subscribes to necessary events
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.AddListener(OnValidPlayerNumber);
        GameManager.instance.MyEvents.UpdateAttemptsEvent.AddListener(OnUpdateAttempts);
        GameManager.instance.MyEvents.EndRoundEvent.AddListener(OnEndRound);
    }

    /// <summary>
    /// Checks which type of endscreen was reached depending on how many guesses the dragon made.
    /// </summary>
    public void OnEndRound()
    {
        // If the Dragon did not go over the AttemptCap, it won, so the _winElements are displayed
        if (_dragonAttempts <= GuessManager.AttemptCap)
        {
            _winElements.SetActive(true);
            _loseElements.SetActive(false);
        }
        // If the Dragon went over the AttemptCap, it lost, so the _loseElements are displayed
        else
        {
            _winElements.SetActive(false);
            _loseElements.SetActive(true);
        }
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
    public void UnsubscribeFromMyEvents()
    {
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.RemoveListener(OnValidPlayerNumber);
        GameManager.instance.MyEvents.UpdateAttemptsEvent.RemoveListener(OnUpdateAttempts);
        GameManager.instance.MyEvents.EndRoundEvent.RemoveListener(OnEndRound);
    }

    /// <summary>
    /// Tells the GameManager that the player wants to play another round.
    /// </summary>
    public void PlayAgain()
    {
        GameManager.instance.InitializeRound();
    }

    /// <summary>
    /// Tells the GameManager that the player does not want to play another round.
    /// </summary>
    public void Quit()
    {
        GameManager.instance.EndGuessingGame();
    }

    /// <summary>
    /// Updates the playerNumber text when the player chooses a valid number.
    /// </summary>
    /// <param name="playerNumber">The player's chosen number.</param>
    public void OnValidPlayerNumber(int playerNumber)
    {
        _yourNumberText.text = $"Your Number: {playerNumber}";
    }

    /// <summary>
    /// Updates the total attempts text when the Dragon makes another guess attempt
    /// </summary>
    /// <param name="attempts">The total number of guess attempts that the Dragon has made.</param>
    public void OnUpdateAttempts(int attempts)
    {
        _attemptsText.text = $"Total Attempts: {attempts}";
        _dragonAttempts = attempts;
    }

    /// <summary>
    /// Used when the player chooses to surrender to the dragon, and that the dragon should kill the player.
    /// </summary>
    public void PlayerSurrenders()
    {
        // Tells GameManager that the player has surrendered and that the dragon can kill him.
        //      Calls the LieDoubleDown function to inform the GameManager,
        //      because that is the event that starts the sequence of events for the dragon to kill the player.
        //
        //      Originally, the player doubling down was the only scenario in which the dragon would kill the player,
        //      so the logic preparing the player to die was tied to this event.
        //      Realistically, the LieDoubleDown event just functions as a "PrepareForPlayerDeath" event.
        GameManager.instance.MyEvents.LieDoubleDown();
    }
}
