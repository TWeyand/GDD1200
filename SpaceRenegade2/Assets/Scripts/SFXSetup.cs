using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SFXSetup : MonoBehaviour
{
    // The audio source attached to this object
    [SerializeField] private AudioSource _audioSource;

    /// <summary>
    /// Plays the AudioClip and deletes the object after the clip has played
    /// </summary>
    /// <param name="clip">The AudioClip being played</param>
    public void PlayAudio(AudioClip clip)
    {
        StartCoroutine(PlaySFX(clip));
    }

    /// <summary>
    /// Plays the AudioClip and deletes the object after the clip has played
    /// </summary>
    /// <param name="clip">The AudioClip being played</param>
    private IEnumerator PlaySFX(AudioClip clip)
    {
        // Plays the AudioClip on the AudioSource
        _audioSource.PlayOneShot(clip);
        // Waits until the clip has been played, then destroys the SFXPlayer object
        yield return new WaitForSeconds(clip.length);
        Destroy(gameObject);
    }
}
