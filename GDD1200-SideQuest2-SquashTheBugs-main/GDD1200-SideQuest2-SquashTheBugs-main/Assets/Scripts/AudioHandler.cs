using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("AudioSources")] 
    [SerializeField] private AudioSource backgroundMusicPlayer;
    [SerializeField] private AudioSource environmentMusicPlayer;
    [SerializeField] private AudioSource btnSFXPlayer;
    [SerializeField] private AudioSource collectSFXPlayer;
    [SerializeField] private AudioSource playerCharacterPlayer;

    [Header("Sounds")] 
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip environmentMusic;
    [SerializeField] private AudioClip btnSFX;
    [SerializeField] private AudioClip collectSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioClip winSFX;

    public void PlayButtonSound()
    {
        btnSFXPlayer.PlayOneShot(btnSFX);
    }

    public void PlayCollectSound()
    {
        collectSFXPlayer.PlayOneShot(collectSFX);
    }

    public void PlayWinSound()
    {
        collectSFXPlayer.PlayOneShot(winSFX);
    }

    public void PlayDeathSound()
    {
        playerCharacterPlayer.PlayOneShot(deathSFX);
    }

}
