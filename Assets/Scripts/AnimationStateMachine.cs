using UnityEngine;

/// <summary>
/// State Machine used to control the Animator of the Dragon based on the Dragon's mood
/// </summary>
[RequireComponent (typeof(Animator))] // Ensures that there is an Animator to use as reference
public class AnimationStateMachine : MonoBehaviour
{
    private int moodIndex; // The index of the mood parameter within the AnimationController

    /// <summary>
    /// The MoodLevels that correspond to the Dragon's Mood state in the Dragon's Animator
    /// </summary>
    public enum MoodLevel
    {
        // !!! DO NOT CHANGE ANY OF THESE VALUES UNLESS YOU CHANGE THE VALUES AND CONDITIONS IN THE ANIMATOR TOO !!!
        // =========================================================================================================
        Happy = 1,      // Starting state. Happy Dragon. Only a few guesses so far.
        Okay = 2,       // Dragon now has angry eyes
        Annoyed = 3,    // Dragon's mouth is ajar
        Irritated = 4,  // Smoke starts to come out of the mouth
        Upset = 5,      // Embers start to come out of the mouth
        Angry = 6,      // Dragon mouth opens with smoke and embers coming out
        Fire = 7,       // Mouth wide open ready to shoot flames
    }

    /// <summary>
    /// Controls the Dragon's animations based on its mood
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// The current mood of the dragon. Usually based on how many incorrect guesses have been made
    /// </summary>
    private MoodLevel mood;
    /// <summary>
    /// The amount of guess attempts the dragon needs to make before its mood changes.
    /// Set to 1.3f at default as that makes it so that the angry expression only shows up on the last guess when the
    /// attempt cap is set to 8.
    /// </summary>
    private const float ATTEMPTS_PER_MOOD_CHANGE = 1.3f;

    private void Awake()
    {
        // Gets the Animator component so that it can be told what the current mood is
        _animator = GetComponent<Animator>();
        // Gets the hash value of the "Mood" string so that that hashvalue can be used for quicker access to the "Mood" state in the Animator
        moodIndex = Animator.StringToHash("Mood");

        // Subscribes to necessary events
        GameManager.instance.MyEvents.UpdateAttemptsEvent.AddListener(OnUpdateAttempts);
        GameManager.instance.MyEvents.ResetRoundEvent.AddListener(OnResetRound);
        GameManager.instance.MyEvents.LieDoubleDownEvent.AddListener(OnLieDoubleDown);
        GameManager.instance.MyEvents.UpdateDragonMoodEvent.AddListener(OnUpdateDragonMood);
    }

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
        GameManager.instance.MyEvents.ResetRoundEvent.RemoveListener(OnResetRound);
        GameManager.instance.MyEvents.LieDoubleDownEvent.RemoveListener(OnLieDoubleDown);
        GameManager.instance.MyEvents.UpdateDragonMoodEvent.RemoveListener(OnUpdateDragonMood);
    }

    /// <summary>
    /// Resets the Dragon's mood to the default MoodLevel.Happy state.
    /// </summary>
    public void ResetMood()
    {
        SetMood(MoodLevel.Happy);
    }

    /// <summary>
    /// When a new guess attempt has been made by the dragon, uses the current attempts amount to calculate what the mood should be,
    /// and then sets the Dragon's mood to that value.
    /// </summary>
    /// <param name="attempts">The total amount of guess attempts that the Dragon has made.</param>
    public void OnUpdateAttempts(int attempts)
    {
        /*  Formula for calculating the Dragon's current mood
         *      The formula makes sure that every mood starting from MoodLevel.Happy occurs for ATTEMPTS_PER_MOOD_CHANGE attempts
         *          before switching to the next MoodLevel
         *      Mathf.Clamp is used to make sure that the MoodLevel must be one of the valid MoodLevel's that occur based on attempt amounts
         *          MoodLevel.Fire is excluded as it occurs based on when the player doubles down on a lie
         *          NOT because of how many attempts have been made.
         *          
         *      Example moodInt sequence where (n = attempts) and (n starts at 1) and (ATTEMPTS_PER_MOOD_CHANGE = 2):
         *          {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 6, 6, 6, 6,..., 6}
         */
        int moodInt = Mathf.Clamp((int)MoodLevel.Happy + (int)((attempts - 1) / ATTEMPTS_PER_MOOD_CHANGE),
                                  (int)MoodLevel.Happy, (int)MoodLevel.Angry);
        
        // Tells the GameManager that the Dragon's mood needs to be updated to (MoodLevel)moodInt
        GameManager.instance.MyEvents.UpdateDragonMood((MoodLevel)moodInt);
    }

    /// <summary>
    /// Resets the dragon's mood when the round resets, so a new fresh round can begin
    /// </summary>
    public void OnResetRound()
    {
        ResetMood();
    }

    /// <summary>
    /// Makes the Dragon at least irritated when the player doubles down on a lie.
    /// This is done just as a visual to show that the Dragon did not like that the player lied to them.
    /// </summary>
    public void OnLieDoubleDown()
    {
        // If the Dragon is not at least Irritated, tells the GameManager to update the Dragon's mood to irritated
        if (mood < MoodLevel.Irritated)
        {
            GameManager.instance.MyEvents.UpdateDragonMood(MoodLevel.Irritated);
        }
    }

    /// <summary>
    /// When anything has happened to change the Dragon's mood, sets the Dragon's mood to whatever the input "newMood" is.
    /// </summary>
    /// <param name="newMood">The new MoodLevel that the Dragon is being updated to.</param>
    public void OnUpdateDragonMood(MoodLevel newMood)
    {
        SetMood(newMood);
    }

    /// <summary>
    /// Sets the Dragon's mood to newMood.
    /// </summary>
    /// <param name="newMood">The MoodLevel the Dragon's mood is being changed to.</param>
    private void SetMood(MoodLevel newMood)
    {
        // Changes the internal state machine to the newMood state
        mood = newMood;
        // Changes the Animator's "Mood" state to the int value of the newMood
        _animator.SetInteger(moodIndex, (int)mood);
    }
}
