using UnityEngine;

/// <summary>
/// Controls the NumberGuesser that guesses the numebr for the Dragon
/// </summary>
public class GuessManager : MonoBehaviour
{
    /* The maximum amount of attempts the dragon has in order to get the player's guess.
     * This is a static field so that the EndScreen can read it to validate whether the Dragon won or lost.
     * 8 is set as the default number, as that felt best after testing. */
    private static int _attemptCap = 8; 
    /// <summary>
    /// The maximum amount of attempts the Dragon has to get the player's guess.
    /// </summary>
    public static int AttemptCap { get { return _attemptCap; } }

    // The comparison values of the Dragon's Guess to the player's target number
    public enum GuessComparison
    {
        LowerGuess = -1,    // Guess was lower than the target
        EqualGuess = 0,     // Guess was the same as the target
        HigherGuess = 1,    // Guess was higher than the target
    }

    // The object that handles all the guessing logic
    private NumberGuesser _guesser;

    private void Start()
    {
        // Instantiates the _guesser
        _guesser = new NumberGuesser();

        // Subscribes to relevant events
        GameManager.instance.MyEvents.SubmitPlayerNumberEvent.AddListener(OnSubmitPlayerNumber);
        GameManager.instance.MyEvents.StartGuessingPhaseEvent.AddListener(OnStartGuessingPhase);
        GameManager.instance.MyEvents.SubmitFeedbackEvent.AddListener(OnSubmitFeedback);
        GameManager.instance.MyEvents.ResetRoundEvent.AddListener(OnResetRound);

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
        GameManager.instance.MyEvents.SubmitPlayerNumberEvent.RemoveListener(OnSubmitPlayerNumber);
        GameManager.instance.MyEvents.StartGuessingPhaseEvent.RemoveListener(OnStartGuessingPhase);
        GameManager.instance.MyEvents.SubmitFeedbackEvent.RemoveListener(OnSubmitFeedback);
        GameManager.instance.MyEvents.ResetRoundEvent.RemoveListener(OnResetRound);
    }

    /// <summary>
    /// Simulates the dragon making a guess.
    /// Does <b>NOT</b> return the Dragon's guess.
    /// </summary>
    public void DragonGuess()
    {
        // If the Dragon has not reached its attempt cap, lets the dragon make another guess
        if (_guesser.TotalGuesses < _attemptCap)
        {
            // Tells the _guesser to make a new guess
            _guesser.MakeGuess();
            Debug.Log($"_guesser made a guess of {_guesser.Guess}");
            Debug.Log($"Attempt #{_guesser.TotalGuesses}");
            // Tells MyEvents what the new guess is
            GameManager.instance.MyEvents.SetGuess(_guesser.Guess);
            // Tells MyEvents what the new total amount of guesses is
            GameManager.instance.MyEvents.UpdateAttempts(_guesser.TotalGuesses);
        }
        // If the Dragon went over its AttemptCap, the player won.
        // As such, the round is ended with the Dragon losing.
        else
        {
            // Updates the dragon's total attempts to a number over the attempt count.
            //      This occurs so that the EndScreen knows that it was reached with an attempt count that was higher than the AttemptCap.
            GameManager.instance.MyEvents.UpdateAttempts(_attemptCap + 1
                                                                 /* '+ 1' exists so that the EndScreen sees that the Dragon went over the cap*/);
            // Tells the GameManager that the round has ended
            GameManager.instance.MyEvents.EndRound();
        }
    }


    /// <summary>
    /// Triggers when the player has given feedback to the _guesser's guess
    /// </summary>
    /// <param name="feedback">The int value of the player's feedback</param>
    public void OnSubmitFeedback(GuessComparison feedback)
    {
        // Checks if the feedback was accurate
        bool accurateFeedback = _guesser.ValidateFeedback((NumberGuesser.GuessComparison)feedback);

        // If the player gave accurate feedback
        if (accurateFeedback)
        {
            Debug.Log("Feedback Received: The player gave accurate feedback");

            // If the guess was correct
            if (feedback == GuessComparison.EqualGuess)
            {
                Debug.Log("The guess was correct");
                // Informs the GameManager that the round is over due to the dragon correctly guessing
                GameManager.instance.MyEvents.EndRound();
            }
            // If the guess was incorrect
            else {
                Debug.Log("The guess was incorrect");
                _guesser.IncorrectGuess();
                // Informs the GameManager that the GuessingPhase needs to restart, because the dragon was incorrect
                GameManager.instance.MyEvents.RestartGuessingPhase();

            }
        }
        else
        {
            Debug.Log("Feedback Received: THE PLAYER LIED");
            // Informs the GameManager that the player lied
            GameManager.instance.MyEvents.PlayerLied();
        }
    }

    /// <summary>
    /// Sets the player's number inside the _guesser
    /// </summary>
    /// <param name="playerNumber">The player's chosen number</param>
    private void SetPlayerNumber(int playerNumber)
    {
        // Sets the playerNumber as the target number that the _guesser is trying to guess
        //      This is needed so the _guesser can validate any feedback that is received by the player
        _guesser.SetTarget(playerNumber);
    }

    /// <summary>
    /// Returns the current number that the Dragon is trying to guess
    /// </summary>
    /// <returns>The current number that the Dragon is trying to guess</returns>
    public int GetPlayerNumber()
    {
        return _guesser.Target;
    }

    /// <summary>
    /// Returns the total guess attempts that the dragon has made.
    /// </summary>
    /// <returns>The total amount of guess attempts that the dragon has made.</returns>
    public int GetTotalAttempts()
    {
        return _guesser.TotalGuesses;
    }

    /// <summary>
    /// Triggers when the player has submitted a number as their target.
    /// Lets the GameManager know if the number was valid.
    /// </summary>
    /// <param name="playerNumber">The number that was submitted by the player</param>
    public void OnSubmitPlayerNumber(int playerNumber)
    {
        // If the number is within the valid range
        if (_guesser.IsInRange(playerNumber))
        {
            Debug.Log($"Received {playerNumber} from the keypad");
            // Sets teh submitted number as the player number
            SetPlayerNumber(playerNumber);
            // Tells MyEvents that a ValidPlayerNumber was accepted
            GameManager.instance.MyEvents.ValidPlayerNumber(playerNumber);
        }
        // If the number is NOT within the valid range
        else
        {
            // Tells MyEvents that an InalidPlayerNumber was rejected
            GameManager.instance.MyEvents.InvalidPlayerNumber();
        }
    }

    /// <summary>
    /// Triggers at the start of a guessing phase
    /// </summary>
    public void OnStartGuessingPhase()
    {
        // Calls DragonGuess() so that the Dragon will make a new guess
        DragonGuess();
    }

    /// <summary>
    /// Triggers when the round is being reset
    /// </summary>
    public void OnResetRound()
    {
        // Resets the Guesser to its starting values so that a fresh round can begin
        _guesser.ResetGuesser();
    }

}
