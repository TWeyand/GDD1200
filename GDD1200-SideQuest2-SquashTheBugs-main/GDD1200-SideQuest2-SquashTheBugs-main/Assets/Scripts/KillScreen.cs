using TMPro;
using UnityEngine;

public class KillScreen : MonoBehaviour
{
    [Header("UI Information")] 
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI respawnText;

    private void Update()
    {
        if (this.gameObject.activeInHierarchy)
        {
            SetRespawnText(PlayerDeath.deathTimer);
        }
    }
    public void SetLivesText(int lives)
    {
        livesText.text = lives;
    }

    private void SetRespawnText(float respawn)
    {
        respawnText.text = $"Respawning in...{respawn:N0}";
    }
}
