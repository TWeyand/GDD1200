using UnityEngine;

/// <summary>
/// Used to band-aid fix initialization problems within the GuessScene
/// </summary>
public class GuessSceneInit : MonoBehaviour
{


    [Tooltip("The GameObject that contains the canvas for the keypadUI.")]
    [SerializeField]
    private GameObject _keypadUI;

    void Start()
    {
        // Turns the keypadUI on and off to make sure the position of the keypad is initialized before the KeypadMover tries to move it
        _keypadUI.SetActive(true);
        _keypadUI.SetActive(false);

        // Initializes the first round of the game
        GameManager.instance.InitializeRound();
    }
}
