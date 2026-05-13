using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    [field: SerializeField] public int ScoreValue { get; set; } = 100;
    public void AddScore()
    {
        ScoreManager.Instance.AddScore(ScoreValue);
    }
}
