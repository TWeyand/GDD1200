using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls how the keypad gets a number input
/// </summary>
public class Keypad : MonoBehaviour
{
    // The max length of the number before the textbox has to wrap.
    private const int MAX_LENGTH = 15;
    // The minimum length of a string before it is 0
    private const int MIN_LENGTH = 1;
    // The value the keypad uses as the player's numebr if the _inputNumber is empty
    private const int EMPTY_INPUT = 0;

    /// <summary>
    /// The text object used as the keypad's display
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI _screen;
    /// <summary>
    /// The number that the keypad currently has stored (As a string).
    /// </summary>
    private string _inputNumber = "";

    /// <summary>
    /// List of all of the buttons that are on the keypad.
    /// </summary>
    [SerializeField]
    private System.Collections.Generic.List<UnityEngine.UI.Button> _buttons;

    private void Start()
    {
        // Updates the screen's text to the current _inputNumber
        SetScreenText();

        // Subscribes to the events needed for this object
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.AddListener(OnValidPlayerNumber);
        GameManager.instance.MyEvents.InvalidPlayerNumberEvent.AddListener(OnInvalidPlayerNumber);
        GameManager.instance.MyEvents.ResetRoundEvent.AddListener(OnResetRound);
    }

    // Clears event subscriptions when object is destroyed
    private void OnDestroy()
    {
        /* Attempts to Unsubscribe from all events
         *      This will not cause any issues even if the MyEvents object is already deleted
         *      so checking for that is unneeded */
        UnsubscribeFromMyEvents();
    }

    /// <summary>
    /// Unsubscribes from all events connected to the MyEvents object.
    /// Unsubscribing from events is important to protect memory leaks.
    /// </summary>
    private void UnsubscribeFromMyEvents()
    {
        GameManager.instance.MyEvents.ValidPlayerNumberEvent.RemoveListener(OnValidPlayerNumber);
        GameManager.instance.MyEvents.InvalidPlayerNumberEvent.RemoveListener(OnInvalidPlayerNumber);
        GameManager.instance.MyEvents.ResetRoundEvent.RemoveListener(OnResetRound);
    }

    /// <summary>
    /// Resets the keypad to its black state
    /// </summary>
    public void ResetKeypad()
    {
        // Empties the _inputNumber and updates the screen display's text
        _inputNumber = "";
        SetScreenText();
        // Sets the screen's text color to white
        _screen.color = Color.white;
    }

    /// <summary>
    /// Adds a string "number" to the end of the keypad's _inputNumber.
    /// </summary>
    /// <param name="number">The number being added to the end of _inputNumber</param>
    public void AddNumber(string number)
    {
        // If the _inputNumber field has not reached its max length.
        if (_inputNumber.Length <= MAX_LENGTH)
        {
            // Adds the number to the end of the _inputNumber
            _inputNumber += number;
            // Updates the screen text based on the current _inputNumber
            SetScreenText();
        }
    }

    /// <summary>
    /// Removes the digit that was most recently added to the _inputNumber 
    /// </summary>
    public void BackSpace()
    {
        // If the _inputNumber has at least two digits to erase 
        if (_inputNumber.Length > MIN_LENGTH)
        {
            // Trims the most recently entered number off of the _inputNumber
            //    This should be the last character in the string array, so we create a substring that includes every character up to the last one
            _inputNumber = _inputNumber.Substring(0, _inputNumber.Length - 1);
        }
        // If the _inputNumber is only one digit or already empty, then set the _inputNumber to an empty string
        else
        {
            _inputNumber = "";
        }

        // Updates the screen text based on the current _inputNumber
        SetScreenText();
    }

    /// <summary>
    /// Submits the inputNumber player number to the GameManager
    /// </summary>
    public void Submit()
    {
        // If the _inputNumber is not empty, submits the _inputNumber's integer value to the GameManager
        if (!String.IsNullOrEmpty(_inputNumber)) {
            // Uses int.Parse(_inputNumber) to convert the string number to an integer
            GameManager.instance.MyEvents.SubmitPlayerNumber(int.Parse(_inputNumber));
        }
        // If the _inputNumber is empty, submits 0 to the GameManager
        else
        {
            // Uses int.Parse(EMPTY_INPUT) to convert the string number to an integer
            GameManager.instance.MyEvents.SubmitPlayerNumber(EMPTY_INPUT);
        }
    }

    /// <summary>
    /// Sets the screen's text to the _inputNumber
    /// </summary>
    private void SetScreenText()
    {
        // If the _inputNumber is not an empty string, sets the screen display to the _inputNumber
        if (!String.IsNullOrEmpty(_inputNumber)) {
            _screen.text = _inputNumber;
        }
        // If the _inputNumber is empty, dispays "0" on the screen
        else
        {
            _screen.text = EMPTY_INPUT.ToString();
        }
        
    }

    /// <summary>
    /// Disables the Keypad from receiving user input.
    /// </summary>
    private void DisableKeypadInput()
    {
        // Goes through each button on the keypad and marks their interactable field as false, so that they cannot be pressed by the player
        foreach (Button button in _buttons)
        {
            button.interactable = false;
        }
    }

    /// <summary>
    /// Enables the Keypad to receive user input.
    /// </summary>
    private void EnableKeypadInput()
    {
        // Goes through each button on the keypad and marks their interactable field as true, so they can be pressed by the player
        foreach (Button button in _buttons)
        {
            button.interactable = true;
        }
    }

    /// <summary>
    /// Disables the keypad once a ValidNumber has been entered.
    /// This is done so that the player cannot try to hit the keypad buttons while it is moving offscreen
    /// </summary>
    /// <param name="playerNumber">The player's number. This is not used in this function and is only here so that it matches the ValidPlayerNumber event</param>
    public void OnValidPlayerNumber(int playerNumber)
    {
        // Sets the screen text color to green as a way to show that the number was accepted
        _screen.color = Color.limeGreen;
        DisableKeypadInput();
    }

    /// <summary>
    /// If an invalid number is entered, resets the keypad so that a new number can be entered.
    /// </summary>
    public void OnInvalidPlayerNumber()
    {
        ResetKeypad();
    }

    /// <summary>
    /// Resets the keypad for a new round.
    /// </summary>
    public void OnResetRound()
    {
        ResetKeypad(); 
        // Enable's keypad input since it will be needed so that the player can enter a new number.
        // This won't cause any issues as the keypad isn't active until the PlayerNumber phase starts.
        EnableKeypadInput();
    }

    
}
