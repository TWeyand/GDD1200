using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const float RESPAWN_INVULNERABILITY = 3.0f;

    public static PlayerManager Instance { get; private set; }

    [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }

    [field: SerializeField] public GameObject PlayerPrefab { get; private set; }

    [field: SerializeField] public static GameObject Player { get; private set; }

    [field: SerializeField] public static int StartingLives { get; private set; } = 3;

    [field: SerializeField] public static int Lives { get; private set; }

    [SerializeField] private float _respawnTime = 3.0f;

    [SerializeField] private TextMeshProUGUI _livesText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPlayer();
        // Sets the Lives amount and the UI counter
        Lives = StartingLives;
        _livesText.text = $"LIVES: {Lives}";
    }

    /// <summary>
    /// When the player dies
    /// </summary>
    public void OnPlayerDeath()
    {
        // Destroys the current Player object
        Destroy(Player);

        // Clears the current Player variable
        Player = null;

        // Decrements lives and updates the UI counter
        Lives--;
        _livesText.text = $"LIVES: {Lives}";

        // If out of lives, tells the GameManager to end the gameplay
        if (Lives <= 0) { GameManager.Instance.OutOfLives(); }
        // If the player is not out of lives, starts the coroutine to respawn the player
        else { StartCoroutine(RespawnCoroutine()); }
    }


    public IEnumerator RespawnCoroutine()
    {
        float timer = _respawnTime;

        DisplayManager.Instance.ShowRespawnCounter();
        DisplayManager.Instance.UpdateRespawnCounter(timer);

        while (Mathf.Round(timer) > 0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            timer -= 1.0f;
            DisplayManager.Instance.UpdateRespawnCounter(timer);
        }

        DisplayManager.Instance.HideRespawnCounter();

        SpawnPlayer();
        StartCoroutine(InvulnerableCoroutine(RESPAWN_INVULNERABILITY));
        StartCoroutine(FlashingCoroutine(RESPAWN_INVULNERABILITY));
    }

    /// <summary>
    /// Spawns the Player facing down at (0,0)
    /// </summary>
    public void SpawnPlayer() 
    {
        Player = GameObject.Instantiate(PlayerPrefab, PlayerSpawnPoint.position, Quaternion.Euler(Vector3.back * 90.0f));
    }

    /// <summary>
    /// Coroutine that makes the Player invulnerable for a short time after respawing
    /// </summary>
    /// <param name="invulnerabilityTime">The time the Player is invulnerable</param>
    /// <returns></returns>
    public IEnumerator InvulnerableCoroutine(float invulnerabilityTime)
    {
        Health health = Player.GetComponent<Health>();
        health.enabled = false;

        yield return new WaitForSeconds(invulnerabilityTime);

        health.enabled = true;
    }

    /// <summary>
    /// Coroutine that makes the PlayerShip flash colors while it is invulnerable
    /// </summary>
    /// <param name="invulnerabilityTime">The time the Player is invulnerable</param>
    /// <returns></returns>
    public IEnumerator FlashingCoroutine(float invulnerabilityTime)
    {
        Player player = Player.GetComponent<Player>();

        // While the player still has invulnerability left
        while (invulnerabilityTime > 0.0f)
        {
            // Flashes the colors of the Player so it is clear that they are still invulnerable
            yield return new WaitForSeconds(0.25f);
            player.DarkenColors();
            yield return new WaitForSeconds(0.25f);
            player.DefaultColors();

            // Decreases the amount of invulnerability time the Player has left
            invulnerabilityTime -= 0.5f;
        }
    }
}
