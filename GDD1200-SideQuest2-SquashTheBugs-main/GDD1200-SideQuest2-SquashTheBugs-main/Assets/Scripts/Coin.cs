using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public delegate void PlayerWins();
    public static event PlayerWins OnPlayerWins;
    
    [SerializeField] private ParticleSystem winVFX;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPlayerWins?.Invoke();
            Instantiate(winVFX, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
