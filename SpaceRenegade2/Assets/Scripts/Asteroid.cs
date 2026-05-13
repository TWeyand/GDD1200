using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Asteroid : MonoBehaviour
{
    public enum Sizes
    {
        Small,
        Medium,
        Big,
    }

    private const float SMALL_SCALE = 0.5f;
    private const float MEDIUM_SCALE = 1.0f;
    private const float BIG_SCALE = 2.0f;

    private const float BIG_MOVE_SPEED = 2.0f;
    private const float MIN_MOVE_SPEED = 0.5f;
    private const float MAX_MOVE_SPEED = 3.0f;

    [field: SerializeField]
    public float CurrentSpeed {  get; private set; } = 0f;

    [field: SerializeField] public Rigidbody2D MyRigidbody {  get; private set; }

    [field: SerializeField] public Sizes Size { get; private set; } = Sizes.Big;

    [field: SerializeField] private GameObject _breakVFX;

    [field: SerializeField] public AudioClip BreakClip { get; private set; }
    [SerializeField] private GameObject SFXPlayer;

    private void Awake()
    {
        SetSize(Size);

        switch (Size) {
            case Sizes.Small:
                RandomizeMovement();
                break;
            case Sizes.Medium:
                RandomizeMovement();
                break;
            case Sizes.Big:
                RandomizeMoveAngle();
                CurrentSpeed = BIG_MOVE_SPEED;
                MyRigidbody.linearVelocity = MyRigidbody.linearVelocity.normalized * BIG_MOVE_SPEED;
                CurrentSpeed = MyRigidbody.linearVelocity.magnitude;
                break;
        }
    }

    public void SetSize(Sizes newSize)
    {
        Size = newSize;

        switch(newSize)
        {
            case Sizes.Small:
                transform.localScale = new Vector3(SMALL_SCALE, SMALL_SCALE, transform.localScale.z); 
                break;
            case Sizes.Medium:
                transform.localScale = new Vector3(MEDIUM_SCALE, MEDIUM_SCALE, transform.localScale.z);
                break;
            case Sizes.Big:
                transform.localScale = new Vector3(BIG_SCALE, BIG_SCALE, transform.localScale.z);
                break;
        }
    }

    private void OnValidate()
    {
        SetSize(Size);
    }

    /// <summary>
    /// Used to randomize the speed and direction of a "child" asteroid that is created by a broken asteroid
    /// </summary>
    public void RandomizeMovement()
    {
        RandomizeMoveAngle();
        RandomizeSpeed();
    }

    private void RandomizeMoveAngle()
    {
        float movementAngle = Random.Range(0.0f, 360.0f);
        MyRigidbody.linearVelocity = MyMath.AngleToVector(movementAngle);
    }

    private void RandomizeSpeed()
    {
        CurrentSpeed = Random.Range(MIN_MOVE_SPEED, MAX_MOVE_SPEED);

        MyRigidbody.linearVelocity = MyRigidbody.linearVelocity.normalized * CurrentSpeed;

        CurrentSpeed = MyRigidbody.linearVelocity.magnitude;
    }

    public void OnDie()
    {
        GameObject.Instantiate(_breakVFX, transform.position, transform.rotation);

        var SFXGenerator = GameObject.Instantiate(SFXPlayer, transform.position, transform.rotation);
        SFXGenerator.GetComponent<SFXSetup>().PlayAudio(BreakClip);

        EnemyManager.Instance.AsteroidBroke(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out Health health))
        {
            health.Damage();
        }
    }
}
