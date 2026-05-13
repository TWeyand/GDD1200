using UnityEngine;
using UnityEngine.UI;

public class ImageColorInitializer : MonoBehaviour
{
    /// <summary>
    /// Stores the current Primary and Secondary colors
    /// </summary>
    [field: SerializeField] public ColorConfig ColorConfig { get; set; }

    /// <summary>
    /// The image that should be the Primary color
    /// </summary>
    [field: SerializeField] public Image PrimaryImage { get; set; }
    /// <summary>
    /// The image that should be the Secondary color
    /// </summary>
    [field: SerializeField] public Image SecondaryImage { get; set; }

    // Updates the colors to be the correct colors according to the ColorConfig
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

    /// <summary>
    /// Updates the colors of PrimaryImage and SecondaryImage to match the corresponding colors in ColorConfig
    /// </summary>
    void UpdateColors()
    {
        if (PrimaryImage != null) PrimaryImage.color = ColorConfig.PrimaryColor;
        if (SecondaryImage != null) SecondaryImage.color = ColorConfig.SecondaryColor;
    }
}
