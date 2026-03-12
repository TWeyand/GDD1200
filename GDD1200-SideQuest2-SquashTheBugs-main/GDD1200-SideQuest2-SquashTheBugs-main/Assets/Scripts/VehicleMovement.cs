using UnityEngine;
using UnityEngine.UI;

public class VehicleMovement : MonoBehaviour
{
    public enum State { MovingRight, MovingLeft}
    public State state;
    [SerializeField] private Sprite[] vehicleSprites;
    public float moveSpeed;

    private SpriteRenderer _vehicleSR;
    private float _rotZ;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _vehicleSR = GetComponent<SpriteRenderer>();
        
        SetVehicleInfo();
    }

    private void FixedUpdate()
    {
        MoveVehicle();
    }

    private void MoveVehicle()
    {
        switch (state)
        {
            case State.MovingRight:
                _rotZ = -90.0f;
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _rotZ);
                _rb.linearVelocity = Vector2.right * moveSpeed;
                if (transform.position.x > 5)
                {
                    Destroy(gameObject);
                }
                break;
            
            case State.MovingLeft:
                _rotZ = 90.0f;
                transform.localEulerAngles= new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, _rotZ);
                _rb.linearVelocity = Vector2.left * moveSpeed;
                if (transform.position.x < -5)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void SetVehicleInfo()
    {
        _vehicleSR.sprite = vehicleSprites[Random.Range(0, vehicleSprites.Length)];
        moveSpeed = Random.Range(3.0f, 3.5f);
    }
}
