using UnityEngine;

/// <summary>
/// The purpose of this script is to organize and play all of the audio information
/// </summary>
public class AudioManager : MonoBehaviour
{
    public AudioClip OpenChestSFX; // SFX for opening treasure
    public AudioClip TeleportOutSFX; // SFX for teleporting out
    public AudioClip TeleportInSFX; // SFX for teleporting in
    public AudioClip[] FootstepsSFX; // SFX for teleporting in
    
    private AudioSource _audioSource; // The source that's playing the audio
    private float _timeSinceLastFootstep; // the amount of time between playing footstep sfx

    private void Start()
    {
        //TODO
        // cache the reference to the audio source
        // Hint: it's on this gameObject
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOpenChestSFX()
    {
        //TODO
        // Play a One shot of the OpenChestSFX
        _audioSource.PlayOneShot(OpenChestSFX);
    }

    public void PlayTeleportOutSFX()
    {
        //TODO
        // Play a One shot of the TeleportOutSFX
        _audioSource.PlayOneShot(TeleportOutSFX);
    }
    
    public void PlayTeleportInSFX()
    {
        //TODO
        // Play a One shot of the TeleportInSFX
        _audioSource.PlayOneShot(TeleportInSFX);
    }
    
    public void PlayFootstepsSFX()
    {
        if (Time.time - _timeSinceLastFootstep >= 0.2f)
        {
            //TODO
            // Set _timeSinceLastFootstep to equal Time.time
            _timeSinceLastFootstep = Time.time;
            // Play a random Footstep Sound from the array of FootstepsSFX, and set its volumeScale to 0.25f
            _audioSource.PlayOneShot(FootstepsSFX[Random.Range(0, FootstepsSFX.Length)], 0.25f);
        }
    }

    
}
