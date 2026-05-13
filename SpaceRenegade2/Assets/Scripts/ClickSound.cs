using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Used to create UI clicks
/// </summary>
public class ClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    // Plays the _audioClip on the _audioSource
    public void OnButtonClick()
    {
        _audioSource.PlayOneShot(_audioClip);
    }
}
