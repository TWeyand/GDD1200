using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ColorConfig", menuName = "Scriptable Objects/ColorConfig")]
public class ColorConfig : ScriptableObject
{
    [field: SerializeField] public Color PrimaryColor { get; private set; }
    [field: SerializeField] public Color SecondaryColor { get; private set; }

    [field: SerializeField] public UnityEvent ChangeColor = new UnityEvent();

    public void SetPrimaryColor(Color newColor)
    {
        PrimaryColor = newColor;
        ChangeColor?.Invoke();
    }

    public void SetSecondaryColor(Color newColor)
    {
        SecondaryColor = newColor;
        ChangeColor?.Invoke();
    }
}
