using UnityEngine;
using UnityEngine.UI;

public class SpriteColorInitializer : MonoBehaviour
{
    [field: SerializeField] public ColorConfig ColorConfig { get; set; }

    [field: SerializeField] public SpriteRenderer PrimarySprite { get; set; }
    [field: SerializeField] public SpriteRenderer SecondarySprite { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateColors();
    }

    private void OnEnable()
    {
        ColorConfig.ChangeColor.AddListener(UpdateColors);
        UpdateColors();
    }

    private void OnDisable()
    {
        ColorConfig.ChangeColor.RemoveListener(UpdateColors);
    }

    // Update is called once per frame
    void UpdateColors()
    {
        if (PrimarySprite != null) PrimarySprite.color = ColorConfig.PrimaryColor;
        if (SecondarySprite != null) SecondarySprite.color = ColorConfig.SecondaryColor;
    }
}
