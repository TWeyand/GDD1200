using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    public static DisplayManager Instance { get; private set; }

    [field: SerializeField] public Canvas ActiveDisplay { get; private set; }

    [SerializeField] private TMPro.TextMeshProUGUI _respawnCounter;
    [SerializeField] private GameObject _respawnText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SetActiveDisplay(ActiveDisplay);

    }

    public void ShowRespawnCounter()
    {
        _respawnText.SetActive(true);
        _respawnCounter.gameObject.SetActive(true);
    }

    public void HideRespawnCounter()
    {
        _respawnText.SetActive(false);
        _respawnCounter.gameObject.SetActive(false);
    }

    public void UpdateRespawnCounter(float count)
    {
        _respawnCounter.text = count.ToString();
    }

    public void SetActiveDisplay(Canvas newActiveDisplay)
    {
        if (ActiveDisplay != null)
        {
            ActiveDisplay.gameObject.SetActive(false);
        }

        newActiveDisplay.gameObject.SetActive(true);

        ActiveDisplay = newActiveDisplay;
    }

    
}
