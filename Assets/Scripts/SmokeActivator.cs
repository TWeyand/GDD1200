using UnityEngine;

/// <summary>
/// Controls the Smoke and Ember effects that come out of the Dragon's mouth
/// </summary>
public class SmokeActivator : MonoBehaviour
{
    /// <summary>
    /// The object that the smoke effects are attached to
    /// </summary>
    [SerializeField]
    private GameObject _smokeParent;
    /// <summary>
    /// The object that the ember effects are attached to[SerializeField]
    /// </summary>
    [SerializeField]
    private GameObject _emberParent;
    /// <summary>
    /// The object that the smoke sfx are attached to
    /// </summary>
    [SerializeField]
    private GameObject _smokeAudio;
    /// <summary>
    /// The object that the ember sfx are attached to
    /// </summary>
    [SerializeField]
    private GameObject _emberAudio;

    /// <summary>
    /// The default position of the smoke and ember effects
    /// </summary>
    [SerializeField]
    private Transform _positionDefault;
    /// <summary>
    /// // The position of the smoke and ember effects when the dragon is angry
    /// </summary>
    [SerializeField]
    private Transform _positionAngry;

    /// <summary>
    /// Used to store the Transform component of the SmokeActivator's GameObject
    /// </summary>
    private Transform _myTransform; 

    public void Awake()
    {
        // Gets the transform component for the SmokeActivator's GameObject
        //  EDIT: I realize now that I could just use gameObject.transform, but I don't have the time to fix that now
        _myTransform = GetComponent<Transform>();

        // Subscribes to event that triggers when the dragon's mood changes
        GameManager.instance.MyEvents.UpdateDragonMoodEvent.AddListener(OnUpdateDragonMood);
        GameManager.instance.MyEvents.PlayerDeathEvent.AddListener(OnPlayerDeath);
        GameManager.instance.MyEvents.ResetRoundEvent.AddListener(OnResetRound);
    }

    public void OnDestroy()
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
        GameManager.instance.MyEvents.UpdateDragonMoodEvent.RemoveListener(OnUpdateDragonMood);
        GameManager.instance.MyEvents.PlayerDeathEvent.RemoveListener(OnPlayerDeath);
        GameManager.instance.MyEvents.ResetRoundEvent.RemoveListener(OnResetRound);
    }

    /// <summary>
    /// Triggers when the Dragon's mood changes.
    /// </summary>
    /// <param name="mood">The dragon's current mood</param>
    public void OnUpdateDragonMood(AnimationStateMachine.MoodLevel mood)
    {
        // Changes what effects are on and where the effects belong depending on the Dragon's mood
        switch (mood)
        {
            case AnimationStateMachine.MoodLevel.Irritated:
                _myTransform.position = _positionDefault.position;
                EnableSmoke();
                DisableEmber();
                break;
            case AnimationStateMachine.MoodLevel.Angry:
                // Makes sure effects align with the Dragon's open mouth
                _myTransform.position = _positionAngry.position;
                EnableSmoke();
                EnableEmber();
                break;
            case AnimationStateMachine.MoodLevel.Upset:
                _myTransform.position = _positionDefault.position;
                EnableSmoke();
                EnableEmber();
                break;
            // The effects in the Fire state should just be whatever the previous state had until the Dragon breathes its flames.
            //  Once the flames are active, the effects are handled elsewhere.
            case AnimationStateMachine.MoodLevel.Fire:
                break;

            // Any other state should have no ember nor smoke and be at the default position
            default:
                _myTransform.position = _positionDefault.position;
                DisableSmoke();
                DisableEmber();
                break;
        }
    }

    /// <summary>
    /// Disables the Smoke particles and audio
    /// </summary>
    public void DisableSmoke()
    {
        _smokeParent.SetActive(false);
        _smokeAudio.SetActive(false);
    }

    /// <summary>
    /// Enables the Smoke particles and audio
    /// </summary>
    public void EnableSmoke()
    {
        _smokeParent.SetActive(true);
        _smokeAudio.SetActive(true);
    }

    /// <summary>
    /// Disables the Ember particles and audio
    /// </summary>
    public void DisableEmber()
    {
        _emberParent.SetActive(false);
        _emberAudio.SetActive(false);
    }

    /// <summary>
    /// Enables the Ember particles and audio
    /// </summary>
    public void EnableEmber()
    {
        _emberParent.SetActive(true);
        _emberAudio.SetActive(true);
    }

    /// <summary>
    /// Disables the smoke and embers once the dragon is breathing actual flames at the player.
    /// They just don't need to be there when they cannot even be seen, and their noise should not be playing anyways.
    /// </summary>
    public void OnPlayerDeath()
    {
        DisableEmber();
        DisableSmoke();
    }

    /// <summary>
    /// Clears the effects so a fresh round can be started
    /// </summary>
    public void OnResetRound()
    {
        DisableSmoke();
        DisableEmber();
    }
}
