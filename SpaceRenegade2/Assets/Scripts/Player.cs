using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ColorConfig ColorConfig;
    [SerializeField] private SpriteRenderer Primary;
    [SerializeField] private SpriteRenderer Secondary;

    public void OnDeath() 
    {
        PlayerManager.Instance.OnPlayerDeath();
    }

    public void DarkenColors()
    {
        Primary.color = Color.darkMagenta;
        Secondary.color = Color.magenta;
    }

    public void DefaultColors()
    {
        Primary.color = ColorConfig.PrimaryColor;
        Secondary.color = ColorConfig.SecondaryColor;
    }
}
