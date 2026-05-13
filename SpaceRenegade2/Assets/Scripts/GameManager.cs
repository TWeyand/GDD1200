using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public static GameObject Player { get; private set; }

    public UnityEvent OutOfLivesEvent = new UnityEvent();

    public UnityEvent ChangeScoreEvent = new UnityEvent();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public void OutOfLives()
    {
        OutOfLivesEvent?.Invoke();
    }

    public void SetPlayer(GameObject player)
    {
        Player = player;
    }

    public void OnChangeScore()
    {
        ChangeScoreEvent?.Invoke();
    }
}
