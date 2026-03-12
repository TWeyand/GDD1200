using UnityEngine;
using UnityEngine.Events;

public class Points : MonoBehaviour
{
    public delegate void CollectPoints(int points);
    public static event CollectPoints OnCollectPoints;

    [SerializeField] private ParticleSystem collectVFX;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollectPoints?.Invoke(100);
            Instantiate(collectVFX, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
