using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [field: SerializeField] public Transform ShootPoint { get; private set; }
    [field: SerializeField] public GameObject Projectile { get; private set; }
    [field: SerializeField] public float ShotDelay { get; private set; } = 0.5f;
    [SerializeField] private PlayerTracker _playerTracker;
    [SerializeField] private EnemyPatrol _enemyPatrol;

    [field: SerializeField] public AudioClip ShotClip { get; private set; }
    [field: SerializeField] public AudioSource Source { get; private set; }

    public void StartShootingAttempt()
    {
        StartCoroutine(ShootingAttempt());
    }


    public IEnumerator ShootingAttempt()
    {
        _playerTracker.enabled = true;
        yield return new WaitForSeconds(ShotDelay);
        GameObject shot = GameObject.Instantiate(Projectile, ShootPoint.position, transform.rotation);
        Source.PlayOneShot(ShotClip);
        _playerTracker.enabled = false;
        yield return new WaitForSeconds(0.25f);
        _enemyPatrol.enabled = true;
    }
}
