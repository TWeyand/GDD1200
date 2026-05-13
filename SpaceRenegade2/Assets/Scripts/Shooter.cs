using System.Collections;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [field: SerializeField] public Transform ShootPoint { get; private set; }
    [field: SerializeField] public GameObject Projectile { get; private set; }

    [field: SerializeField] public float ShotCooldown { get; private set; } = 0.3f;

    private bool _canShoot = true;

    [field: SerializeField] public AudioClip ShotClip { get; private set; }
    [field: SerializeField] public AudioSource Source { get; private set; }

    private Coroutine ShootingCoroutine;

    void OnEnable()
    {
        MyInputs.PlayerShoot.started += OnShoot;
    }

    private void OnDisable()
    {
        MyInputs.PlayerShoot.started -= OnShoot;
    }

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (_canShoot)
        {
            StartCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {
        while (MyInputs.PlayerShoot.IsPressed())
        {
            _canShoot = false;
            GameObject shot = GameObject.Instantiate(Projectile, ShootPoint.position, transform.rotation);
            Source.PlayOneShot(ShotClip);
            yield return new WaitForSeconds(ShotCooldown);
        }

        _canShoot = true;
    }
}
