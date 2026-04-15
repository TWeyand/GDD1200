using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// An extension of the GameManager that is used to store Events that will be broadly used across the game.
/// Essentially just exists so the GameManager script is not clogged up by Event code.
/// 
/// There were definitely other ways to do event management (Event Channels / Event Bus) that may have been better, 
/// but this works for the scale of the project.
/// </summary>
public class MyEvents : MonoBehaviour
{
    /// <summary>
    /// Invoke when a fresh round is ready to be started.
    /// Subscribe if you need to know when a fresh round has started.
    /// </summary>
    public UnityEvent StartPlayerNumberPhaseEvent;
    /// <summary>
    /// Invoke when a player submits a number into the keypad.
    /// Subscribe if you need to know when the player has submitted a number.
    /// </summary>
    public UnityEvent<int> SubmitPlayerNumberEvent;
    /// <summary>
    /// Invoke when the player submits an invalid target number
    /// Subscribe if you need to know when the player entered an invalid number.
    /// </summary>
    public UnityEvent InvalidPlayerNumberEvent;
    /// <summary>
    /// Invoke when the player submits a valid target number
    /// Subscribe if you need to know when the player entered a valid number.
    /// </summary>
    public UnityEvent<int> ValidPlayerNumberEvent;
    /// <summary>
    /// !!!GAME MANAGER ONLY!!!
    /// Invoke when the PlayerPhase is ending, but the GuessingPhase has not yet started
    /// Subscribe if you need to perform actions before the guessing phase starts.
    ///     EXAMPLE: Keypad moving offscreen before the GuessingPhase begins.
    /// </summary>
    public UnityEvent EndPlayerNumberPhaseEvent;

    /// <summary>
    /// Invoke when an instance of a GuessingPhase begins
    /// Subscribe if you need to know when a new GuessingPhase instance begins.
    ///     !!! IMPORTANT NOTE: A new GuessingPhase instace begins each time the dragon makes a new guess
    /// </summary>
    public UnityEvent StartGuessingPhaseEvent;
    /// <summary>
    /// !!!GAME MANAGER ONLY!!!
    /// Invoke when the GuessingPhase is being restarted.
    /// Subscribe if you need to know when a new GuessingPhase instance begins.
    /// </summary>
    public UnityEvent RestartGuessingPhaseEvent;
    /// <summary>
    /// Invoke when setting the current Guess that the Dragon is making.
    /// Subscribe when you need to know when the Dragon has made a new Guess.
    /// </summary>
    public UnityEvent<int> SetGuessEvent;

    /// <summary>
    /// Invoke when the player lies to the Dragon about their feedback.
    /// Subscribe if you need to know when the player has lied.
    /// </summary>
    public UnityEvent PlayerLiedEvent;
    /// <summary>
    /// Invoke when the player retracts the lie about their feedback.
    /// Subscribe if you need to know when the player retracts their lie.
    /// </summary>
    public UnityEvent RetractLieEvent;
    /// <summary>
    /// Invoke when the player doubles down on the lie about their feedback.
    /// Subscribe if you need to know when the player doubles down on their lie.
    /// </summary>
    public UnityEvent LieDoubleDownEvent;
    /// <summary>
    /// Invoke when the Dragon is going to start breathing fire and killing the player.
    /// Subscribe if you need to know when the Dragon starts actually breathing fire
    /// </summary>
    public UnityEvent PlayerDeathEvent;

    /// <summary>
    /// Invoke when the player has submitted feedback to a player's guess.
    /// Passes a GuessManager.GuessComparison that correlates to what type of feedback was submitted.
    /// Subscribe if you need to know when an object needs to know when the player has submitted feedback.
    /// </summary>
    public UnityEvent<GuessManager.GuessComparison> SubmitFeedbackEvent;
    /// <summary>
    /// Invoke when the round has ended.
    /// Subscribe if you need to know when the round has ended.
    /// </summary>
    public UnityEvent EndRoundEvent;
    /// <summary>
    /// Invoke when the Round needs to be reset so a new round can start.
    /// Subscribe if you need to know when to reset everything to its default values so that a fresh new round can be started.
    /// </summary>
    public UnityEvent ResetRoundEvent;

    /// <summary>
    /// Invoke when the Dragon's mood changes.
    /// Subscribe if you need to know when the dragon's mood changes.
    /// </summary>
    public UnityEvent<AnimationStateMachine.MoodLevel> UpdateDragonMoodEvent;
    /// <summary>
    /// Invoke when the Dragon's attempt counter has changed. 
    /// Passes the new attempts amount.
    /// Subscribe if you need an up-to-date value of the Dragon's guess attempts.
    /// </summary>
    public UnityEvent<int> UpdateAttemptsEvent;

    #region Event Invokers
    /// <summary>
    /// Informs the GameManager to start the round.
    /// </summary>
    public void StartPlayerNumberPhase()
    {
        Debug.Log("INVOKED EVENT: StartPlayerNumberPhase");
        StartPlayerNumberPhaseEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that round has ended.
    /// Used when the Dragon has guessed correctly.
    /// </summary>
    public void EndRound()
    {
        Debug.Log("INVOKED EVENT: EndRound");
        EndRoundEvent?.Invoke(); 
    }

    /// <summary>
    /// Informs the GameManager that the round needs to be reset to a fresh state.
    /// </summary>
    public void ResetRound()
    {
        Debug.Log("INVOKED EVENT: ResetRound");
        ResetRoundEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that the player's number was invalid.
    /// </summary>
    public void InvalidPlayerNumber()
    {
        Debug.Log("INVOKED EVENT: InvalidPlayerNumber");
        InvalidPlayerNumberEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that the player's number was valid
    /// </summary>
    /// <param name="playerNumber">The player's number</param>
    public void ValidPlayerNumber(int playerNumber)
    {
        Debug.Log($"INVOKED EVENT: ValidPlayerNumber(playerNumber: {playerNumber})");
        ValidPlayerNumberEvent?.Invoke(playerNumber);
    }

    /// <summary>
    /// Informs the GameManager that player has submitted a number as their target.
    /// </summary>
    /// <param name="playerNumber">The player's submitted number.</param>
    public void SubmitPlayerNumber(int playerNumber)
    {
        Debug.Log($"INVOKED EVENT: SubmitPlayerNumber(playerNumber: {playerNumber})");
        SubmitPlayerNumberEvent?.Invoke(playerNumber);
    }

    /// <summary>
    /// Informs the GameManager that the PlayerNumberPhase is complete and the next GamePhase can begin.
    /// </summary>
    public void EndPlayerNumberPhase()
    {
        Debug.Log("INVOKED EVENT: EndPlayerNumberPhase");
        EndPlayerNumberPhaseEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that a new instance of the GuessingPhase needs to be started.
    /// Used before the dragon makes a new guess.
    /// </summary>
    public void StartGuessingPhase()
    {
        Debug.Log("INVOKED EVENT: StartGuessingPhase");
        StartGuessingPhaseEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that the GuessingPhase needs to be restarted.
    /// Used when the dragon guesses incorrectly.
    /// </summary>
    public void RestartGuessingPhase()
    {
        Debug.Log("INVOKED EVENT: RestartGuessingPhase");
        RestartGuessingPhaseEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that guess value needs to be set to a new value.
    /// </summary>
    /// <param name="guess">The new guess value.</param>
    public void SetGuess(int guess)
    {
        Debug.Log($"INVOKED EVENT: SetGuess(guess: {guess}");
        SetGuessEvent?.Invoke(guess);
    }

    /// <summary>
    /// Informs the GameManager that the player lied about their feedback.
    /// </summary>
    public void PlayerLied()
    {
        Debug.Log("INVOKED EVENT: PlayerLied");
        PlayerLiedEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that the player retracted their lie.
    /// </summary>
    public void RetractLie()
    {
        Debug.Log("INVOKED EVENT: RetractLie");
        RetractLieEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that the player doubled down on their lie.
    /// </summary>
    public void LieDoubleDown()
    {
        Debug.Log("INVOKED EVENT: LieDoubleDown");
        LieDoubleDownEvent?.Invoke();
    }

    /// <summary>
    /// Informs the GameManager that the dragon is killing the player.
    /// </summary>
    public void PlayerDeath()
    {
        Debug.Log("INVOKED EVENT: PlayerDeath");
        PlayerDeathEvent?.Invoke();
    }


    /* DEPRECATED FUNCTION - ONLY REMAINS IN COMMENTS IN CASE I NEED IT LATER FOR SOME REASON
     * ======================================================================================

    /// <summary>
    /// Informs the GameManager that the player has submitted feedback to the dragon's guess.
    /// </summary>
    /// <param name="feedback">The player's feedback value.</param>
    public void SubmitFeedback(int feedback)
    {
        Debug.Log($"INVOKED EVENT: SubmitFeedback(feedback: {feedback})");
        SubmitFeedbackEvent?.Invoke(feedback);
    }
    */

    /// <summary>
    /// Informs the GameManager that Dragon's guess was higher than the target.
    /// </summary>
    public void HigherGuessFeedback()
    {
        Debug.Log("INVOKED EVENT: SubmitFeedback(feedback: GuessComparison.HigherGuess)");
        SubmitFeedbackEvent?.Invoke(GuessManager.GuessComparison.HigherGuess);
    }

    /// <summary>
    /// Informs the GameManager that the Dragon's guess was correct.
    /// </summary>
    public void EqualGuessFeedback()
    {
        Debug.Log("INVOKED EVENT: SubmitFeedback(feedback: GuessComparison.EqualGuess)");
        SubmitFeedbackEvent?.Invoke(GuessManager.GuessComparison.EqualGuess);
    }

    /// <summary>
    /// Informs the GameManager that the Dragon's guess was lower than the target.
    /// </summary>
    public void LowerGuessFeedback()
    {
        Debug.Log("INVOKED EVENT: SubmitFeedback(feedback: GuessComparison.LowerGuess)");
        SubmitFeedbackEvent?.Invoke(GuessManager.GuessComparison.LowerGuess);
    }

    /// <summary>
    /// Informs the GameManager that the dragon's mood has changed.
    /// </summary>
    /// <param name="mood">The dragon's mood level.</param>
    public void UpdateDragonMood(AnimationStateMachine.MoodLevel mood)
    {
        Debug.Log($"INVOKED EVENT: UpdateDragonMood(mood: {mood})");
        UpdateDragonMoodEvent?.Invoke(mood);
    }

    /// <summary>
    /// Informs the GameManager that the number of guess attempts has been updated.
    /// </summary>
    /// <param name="attempts">The number of guess attempts made.</param>
    public void UpdateAttempts(int attempts)
    {
        Debug.Log($"INVOKED EVENT: UpdateAttempts(attempts: {attempts})");
        UpdateAttemptsEvent?.Invoke(attempts);
    }
    #endregion

    /// <summary>
    /// Clears all listeners from every event in this MyEvents object
    /// </summary>
    private void ClearAllEventListeners()
    {
        StartPlayerNumberPhaseEvent.RemoveAllListeners();
        SubmitPlayerNumberEvent.RemoveAllListeners();
        InvalidPlayerNumberEvent.RemoveAllListeners();
        ValidPlayerNumberEvent.RemoveAllListeners();
        EndPlayerNumberPhaseEvent.RemoveAllListeners();

        StartGuessingPhaseEvent.RemoveAllListeners();
        RestartGuessingPhaseEvent.RemoveAllListeners();
        SetGuessEvent.RemoveAllListeners();

        PlayerLiedEvent.RemoveAllListeners();
        RetractLieEvent.RemoveAllListeners();
        LieDoubleDownEvent.RemoveAllListeners();
        PlayerDeathEvent.RemoveAllListeners();

        SubmitFeedbackEvent.RemoveAllListeners();
        EndRoundEvent.RemoveAllListeners();
        ResetRoundEvent.RemoveAllListeners();

        UpdateDragonMoodEvent.RemoveAllListeners();
        UpdateAttemptsEvent.RemoveAllListeners();
    }

    /// <summary>
    /// Clears all events of their listeners when destroyed.
    /// According to the internet, this technically is unnecessary, but I'm paranoid.
    /// </summary>
    private void OnDestroy()
    {
        ClearAllEventListeners();
    }

}
