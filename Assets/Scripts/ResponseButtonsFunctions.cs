using UnityEngine;

/// <summary>
/// Functions that correlate to the response buttons to the Dragon's guess
/// </summary>
public class ResponseButtonsFunctions : MonoBehaviour
{
    /// <summary>
    /// Used to tell the GameManager that the guess was correct
    /// </summary>
    public void Correct()
    {
        GameManager.instance.MyEvents.EqualGuessFeedback();
    }

    /// <summary>
    /// Used to tell the GameManager that the guess was lower than the target
    /// </summary>
    public void TooLow()
    {
        GameManager.instance.MyEvents.LowerGuessFeedback();
    }

    /// <summary>
    /// Used to tell the GameManager that the guess was higher than the target
    /// </summary>
    public void TooHigh()
    {
        GameManager.instance.MyEvents.HigherGuessFeedback();
    }
}
