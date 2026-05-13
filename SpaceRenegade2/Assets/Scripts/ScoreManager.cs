using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Canvas _loseScreen;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    public int Score { get; private set; } = 0;

    void Awake() 
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        GameManager.Instance.OutOfLivesEvent.AddListener(OnOutOfLives);
        SetScore(0);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OutOfLivesEvent.RemoveListener(OnOutOfLives);
    }

    public void AddScore(int addedScore)
    {
        Score += addedScore;
        _scoreText.text = $"SCORE: {Score}";
        _finalScoreText.text = Score.ToString();
    }

    public void SetScore(int newScore)
    {
        Score = newScore;
        _scoreText.text = $"SCORE: {Score}";
        _finalScoreText.text = Score.ToString();
    }

    public void OnOutOfLives()
    {
        DisplayManager.Instance.SetActiveDisplay(_loseScreen);
    }
}
