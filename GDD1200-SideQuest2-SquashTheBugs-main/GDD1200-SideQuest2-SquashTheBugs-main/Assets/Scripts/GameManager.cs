using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Information")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject killScreen;
    [SerializeField] private GameObject smushBubble;
    [SerializeField] private GameObject playerImage;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("Player Input")]
    [SerializeField] private GameObject inputButtons;

    [Header("Player Information")] 
    [SerializeField] private GameObject player;
    
    [Header("General Game Information")]
    [SerializeField] private AudioHandler audioHandler;

    private Vector3 playerStartPosition;
    private CircleCollider2D playerCollider;

    private KillScreen _killUI;
    
    private int lives = 3;
    private int score;

    private void OnEnable()
    {
        PlayerDeath.OnKillPlayer += DealDamage;
        PlayerDeath.OnRevivePlayer += ResetPlayer;
        Coin.OnPlayerWins += WinGame;
        Points.OnCollectPoints += AddScore;
    }

    private void OnDisable()
    {
        PlayerDeath.OnKillPlayer -= DealDamage;
        PlayerDeath.OnRevivePlayer -= ResetPlayer;
        Coin.OnPlayerWins -= WinGame;
        Points.OnCollectPoints -= AddScore;
    }

    private void Start()
    {
        SetUpGame();
    }

    private void SetUpGame()
    {
        inputButtons.SetActive(true);
        playerCollider = player.GetComponent<CircleCollider2D>();
        _killUI = killScreen.GetComponent<KillScreen>();
        playerStartPosition = player.transform.position;
        playerImage.SetActive(true);
        killScreen.SetActive(false);
        smushBubble.SetActive(false);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        UpdateLives();
        UpdateScore();
    }

    private void WinGame()
    {
        inputButtons.SetActive(false);
        audioHandler.PlayWinSound();
        winScreen.SetActive(true);
    }

    private void LoseGame()
    {
        inputButtons.SetActive(false);
        loseScreen.SetActive(true);
    }

    private void SmushDisplay()
    {
        if (lives > 0)
        {
            inputButtons.SetActive(false);
            killScreen.SetActive(true);
            smushBubble.SetActive(true);
            _killUI.SetLivesText(lives);
        }
    }

    private void ResetPlayer()
    {
        inputButtons.SetActive(true);
        killScreen.SetActive(false);
        smushBubble.SetActive(false);
        player.transform.position = playerStartPosition;
        playerCollider.enabled = true;
        playerImage.SetActive(true);
    }

    private void DealDamage()
    {
        playerImage.SetActive(false);
        audioHandler.PlayDeathSound();
        playerCollider.enabled = false;
        lives--;
        UpdateLives();
        if (lives <= 0)
        {
            LoseGame();
            return;
        }
        SmushDisplay();
    }

    private void AddScore(int amount)
    {
        audioHandler.PlayCollectSound();
        // TODO
        // need to add score to the amount here
        UpdateScore();
    }

    private void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
