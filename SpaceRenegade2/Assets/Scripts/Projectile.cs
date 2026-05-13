using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [field: SerializeField] public int DamageAmount { get; private set; } = 1;

    [field: SerializeField] public float Lifespan { get; private set; } = 1.0f;

    [field: SerializeField] public float MoveSpeed { get; private set; } = 5.0f;

    [SerializeField] private GameObject _splashVFX;
    [SerializeField] private bool _playerProjectile = true;

    private Rigidbody2D _rigidbody;

    private Vector2 _screenBounds;

    private void OnEnable()
    {
        Destroy(gameObject, Lifespan);

        if(TryGetComponent<Rigidbody2D>(out _rigidbody))
        {
            _rigidbody.linearVelocityX = Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad) * MoveSpeed;
            _rigidbody.linearVelocityY = Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad) * MoveSpeed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.TryGetComponent<Health>(out Health health))
        {
            if (_playerProjectile && other.gameObject.TryGetComponent<ScoreComponent>(out ScoreComponent scoreComponent))
            {
                scoreComponent.AddScore();
            }
            health.Damage();  
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(_splashVFX, transform.position, Quaternion.identity);
    }

    private void Start()
    {
        _screenBounds = GetScreenBounds(Camera.main);
    }

    private void Update()
    {
        ScreenWarp();
    }

    public void ScreenWarp()
    {
        var position = transform.position;

        // horizontal warp
        if (position.x > _screenBounds.x)
        {
            position.x = -_screenBounds.x;
        }
        else if (position.x < -_screenBounds.x)
        {
            position.x = _screenBounds.x;
        }

        // vertical warp
        if (position.y > _screenBounds.y)
        {
            position.y = -_screenBounds.y;
        }
        else if (position.y < -_screenBounds.y)
        {
            position.y = _screenBounds.y;
        }

        transform.position = position;
    }

    public Vector2 GetScreenBounds(Camera cam)
    {
        var screenTopRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        return new Vector2(screenTopRight.x, screenTopRight.y);
    }
}
