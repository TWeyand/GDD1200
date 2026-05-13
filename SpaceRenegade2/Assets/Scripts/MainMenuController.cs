using System;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private ColorConfig _colorConfig;

    [field: SerializeField] public Canvas MainDisplay { get; private set; }
    [field: SerializeField] public Canvas ControlsDisplay { get; private set; }
    [field: SerializeField] public Canvas CustomizeDisplay { get; private set; }


    public void SetPrimaryColor(Color color)
    {
        _colorConfig.SetPrimaryColor(color);
    }

    public void SetSecondaryColor(Color color)
    {
        _colorConfig.SetSecondaryColor(color);
    }

    public void ActivateMainDisplay()
    {
        DisplayManager.Instance.SetActiveDisplay(MainDisplay);
    }

    public void ActivateControlsDisplay()
    {
        DisplayManager.Instance.SetActiveDisplay(ControlsDisplay);
    }

    public void ActivateCustomizeDisplay()
    {
        DisplayManager.Instance.SetActiveDisplay(CustomizeDisplay);
    }
}
