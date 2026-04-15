using UnityEngine;

/// <summary>
/// Used by the Dragon Animator to trigger the ReadyToBreathe event once that Dragon animation has reached that point.
/// </summary>
public class FireAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// Tells GameManager that the Dragon is ready to start breathing fire to kill the player.
    /// </summary>
    private void BreatheFire()
    {
        GameManager.instance.MyEvents.PlayerDeath();
    }
}
