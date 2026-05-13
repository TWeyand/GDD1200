using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    [field: SerializeField] public float TurnSpeed { get; private set; } = 360.0f;
    [field: SerializeField] public float ThrustSpeed { get; private set; } = 3.0f;
    [field: SerializeField] public float MaxThrustSpeed { get; private set; } = 20.0f;
    [field: SerializeField] public Rigidbody2D MyRigidbody { get; private set; }

    public UnityEngine.Events.UnityEvent ThrustPerformed;

    public UnityEngine.Events.UnityEvent ThrustCanceled;

    private Vector2 _angleComponents = new Vector2();


    public float TurnValue { get; set; }
    private bool _thrustValue;
    public bool ThrustValue { 
        get
        {
            return _thrustValue;
        }
        set 
        {
            if (_thrustValue != value)
            {
                if (value)
                {
                    ThrustPerformed?.Invoke();
                    Debug.Log($"Ship Thrust Activated");
                }
                else
                {
                    ThrustCanceled?.Invoke();
                    Debug.Log($"Ship Thrust Canceled");
                }

                _thrustValue = value;
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        HandleTurn();
        HandleThrust();
    }

    private void HandleTurn()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + TurnSpeed * TurnValue * Time.deltaTime);

        MyMath.AngleToVector(ref _angleComponents, transform.eulerAngles.z);
    }

    private void HandleThrust()
    {
        if (ThrustValue)
        {
            float thrustMagnitude = Mathf.Clamp(MyRigidbody.linearVelocity.magnitude + (ThrustSpeed * Time.deltaTime), 0.0f, MaxThrustSpeed);

            MyRigidbody.linearVelocityX = thrustMagnitude * _angleComponents.x;
            MyRigidbody.linearVelocityY = thrustMagnitude * _angleComponents.y;
        }
        else
        {
            
        }
    }
}