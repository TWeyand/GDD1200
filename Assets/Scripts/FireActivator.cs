using UnityEngine;

public class FireActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject _fireParticles;
    [SerializeField]
    private GameObject _fireAudio;

    public void Awake()
    {
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
    public void UnsubscribeFromMyEvents()
    {
        GameManager.instance.MyEvents.PlayerDeathEvent.RemoveListener(OnPlayerDeath);
    }

    /// <summary>
    /// Turns on fire breath so that the Dragon kills the player
    /// </summary>
    public void OnPlayerDeath()
    {
        _fireParticles.SetActive(true);
        _fireAudio.SetActive(true);
    }
}
