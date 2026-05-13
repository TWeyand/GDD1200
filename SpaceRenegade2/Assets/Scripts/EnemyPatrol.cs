using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [field: SerializeField] public Rigidbody2D MyRigidbody {  get; private set; }
    [field: SerializeField] public PlayerTracker PlayerTracker { get; private set; }
    [field: SerializeField] public EnemyShooter EnemyShooter { get; private set; }
    /// <summary>
    /// The Minimum time the enemy ship moves while patrolling
    /// </summary>
    [field: SerializeField] public float MinPatrol { get; private set; } = 0.1f;
    /// <summary>
    /// The Maximum time the enemy ship moves while patrolling
    /// </summary>
    [field: SerializeField] public float MaxPatrol { get; private set; } = 1.0f;

    [SerializeField] private float _moveSpeed = 2.0f;

    void OnEnable()
    {
        RandomizeMoveAngle();
        MyRigidbody.linearVelocity *= _moveSpeed;

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(MinPatrol, MaxPatrol));
        PlayerTracker.enabled = true;
        MyRigidbody.linearVelocity = Vector2.zero;
        EnemyShooter.StartShootingAttempt();
        this.enabled = false;
    }

    private void RandomizeMoveAngle()
    {
        float movementAngle = Random.Range(0.0f, 360.0f);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, movementAngle);
        MyRigidbody.linearVelocity = MyMath.AngleToVector(movementAngle);
    }

}
