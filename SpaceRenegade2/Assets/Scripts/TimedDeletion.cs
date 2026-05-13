using UnityEngine;

/// <summary>
/// Add to a GameObject so that it will delete itself after a certain amount of seconds of this component being enabled
/// </summary>
public class TimedDeletion : MonoBehaviour
{
    [field: SerializeField] public float DeletionDelay { get; set; } = 1.0f;

    private void OnEnable()
    {
        Destroy(gameObject, DeletionDelay);
    }
}
