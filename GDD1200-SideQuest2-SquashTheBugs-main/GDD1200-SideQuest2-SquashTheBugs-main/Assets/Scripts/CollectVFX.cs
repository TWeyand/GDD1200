using UnityEngine;

public class CollectVFX : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }
    
    private void Update()
    {
        if (!_particle.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
