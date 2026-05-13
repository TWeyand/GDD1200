using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    [SerializeField] private ColorConfig _colorConfig;

    public enum Colors
    {
        Red,
        Blue,
        Green,
        Yellow,
        White,
        Black,
    }

    [SerializeField] private UnityEngine.Color MyRed = UnityEngine.Color.red;
    [SerializeField] private UnityEngine.Color MyGreen = UnityEngine.Color.limeGreen;
    [SerializeField] private UnityEngine.Color MyBlue = UnityEngine.Color.lightBlue;
    [SerializeField] private UnityEngine.Color MyYellow = UnityEngine.Color.yellow;
    [SerializeField] private UnityEngine.Color MyWhite = UnityEngine.Color.white;
    [SerializeField] private UnityEngine.Color MyGray = UnityEngine.Color.darkGray;

    public void SetPrimaryRed() { SetPrimaryColor(Colors.Red); }
    public void SetPrimaryBlue() { SetPrimaryColor(Colors.Blue); }
    public void SetPrimaryGreen() { SetPrimaryColor(Colors.Green); }
    public void SetPrimaryYellow() { SetPrimaryColor(Colors.Yellow); }
    public void SetPrimaryWhite() { SetPrimaryColor(Colors.White); }
    public void SetPrimaryGray() { SetPrimaryColor(Colors.Black); }

    public void SetSecondaryRed() { SetSecondaryColor(Colors.Red); }
    public void SetSecondaryBlue() { SetSecondaryColor(Colors.Blue); }
    public void SetSecondaryGreen() { SetSecondaryColor(Colors.Green); }
    public void SetSecondaryYellow() { SetSecondaryColor(Colors.Yellow); }
    public void SetSecondaryWhite() { SetSecondaryColor(Colors.White); }
    public void SetSecondaryGray() { SetSecondaryColor(Colors.Black); }

    public void SetPrimaryColor(Colors color)
    {
        UnityEngine.Color newColor;

        switch (color)
        {
            case Colors.Red:
                newColor = MyRed;
                break;
            case Colors.Blue:
                newColor = MyBlue;
                break;
            case Colors.Green:
                newColor = MyGreen;
                break;
            case Colors.Yellow:
                newColor = MyYellow;
                break;
            case Colors.White:
                newColor = MyWhite;
                break;
            case Colors.Black:
                newColor = MyGray;
                break;
            default:
                newColor = MyRed;
                break;
        }

        _colorConfig.SetPrimaryColor(newColor);
    }

    public void SetSecondaryColor(Colors color)
    {
        UnityEngine.Color newColor;

        switch (color)
        {
            case Colors.Red:
                newColor = MyRed;
                break;
            case Colors.Blue:
                newColor = MyBlue;
                break;
            case Colors.Green:
                newColor = MyGreen;
                break;
            case Colors.Yellow:
                newColor = MyYellow;
                break;
            case Colors.White:
                newColor = MyWhite;
                break;
            case Colors.Black:
                newColor = MyGray;
                break;
            default:
                newColor = MyRed;
                break;
        }

        _colorConfig.SetSecondaryColor(newColor);
    }
}
