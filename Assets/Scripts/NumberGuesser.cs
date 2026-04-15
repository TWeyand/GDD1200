using UnityEngine;

/// <summary>
/// The logic for the Dragon's number guessing behaviors
/// </summary>
public class NumberGuesser
{
    // The comparison results
    public enum GuessComparison
    {
        LowerGuess = -1,    // Guess was lower than the target
        EqualGuess = 0,     // Guess was the same as the target
        HigherGuess = 1,    // Guess was higher than the target
    }

    private const int DEFAULT_UPPER = 100;  // Default upperRange
    private const int DEFAULT_LOWER = 1;    // Default lowerRange

    private const int NO_GUESSES = 0;       // Used to set the totalGuesses counter to 0

    private int _upperRange;    // The highest value that the target can be at the start. Also used to define the upper limit that the guesser can guess.
    private int _lowerRange;    // The lowest value that the target can be at the start. Also used to define the lower limit that the guesser can guess.

    // The total number of guesses that the NumberGuesser has made.
    private int _totalGuesses;
    /// <summary>
    /// The total number of guess attempts that the NumberGuesser has made.
    /// </summary>
    public int TotalGuesses { get { return _totalGuesses; } }

    // The most recent guess that the NumberGuesser has made
    private int _guess;
    /// <summary>
    /// The NumberGuesser's most recent guess.
    /// </summary>
    public int Guess { get { return _guess; } }

    // The number that the NumberGuesser is trying to guess.
    private int _target;
    /// <summary>
    /// The number that the NumberGuesser is trying to guess.
    /// </summary>
    public int Target { get { return _target; } }

    // Marks if the target value has been set yet.
    private bool _targetIsSet = false;

    /// <summary>
    /// Creates a NumberGuesser with an inclusive range of 1-100
    /// </summary>
    public NumberGuesser()
    {
        _upperRange = DEFAULT_UPPER; 
        _lowerRange = DEFAULT_LOWER;
    }

    /// <summary>
    /// Creates a NumberGuesser with an inclusive range of 1 - {upperRange}
    /// </summary>
    /// <param name="upperRange">The upper limit (Inclusive) that the target value can be.</param>
    public NumberGuesser(int upperRange)
    {
        _upperRange = upperRange; 
        _lowerRange = DEFAULT_LOWER;
    }

    /// <summary>
    /// Sets the target value that the NumberGuesser is trying to guess.
    /// </summary>
    /// <param name="target">The target value.</param>
    public void SetTarget(int target)
    {
        _target = target;
        _targetIsSet = true;
    }

    /// <summary>
    /// Makes a guess based on the current range.
    /// </summary>
    public void MakeGuess()
    {
        // Error message in case the Target has not been set yet
        if (!_targetIsSet) { Debug.LogError("NumberGuesser cannot run the MakeGuess() method if the Target number has not been set."); }

        // Gets a random number between _lowerRange and _upperRange and sets it as the _guess
        //      The '+ 1' is due to the _upperRange not being inclusive in the Random.Range function
        _guess = Random.Range(_lowerRange, _upperRange + 1);
        // Increments the totalGuesses that the number guesser has made
        _totalGuesses++;
    }

    /// <summary>
    /// Compares the NumberGuesser's guess to the target number that the NumberGuesser is trying to guess.
    /// </summary>
    /// <param name="target"></param>
    /// <returns>Returns int value depending on the comparison result./n
    /// -1 if the guess was lower than the target./n
    /// 0 If the guess was the same as the target./n
    /// 1 If the guess was highet than the target.
    /// </returns>
    public GuessComparison CompareGuess()
    {
        // Error message in case the Target has not been set yet
        if (!_targetIsSet) { Debug.LogError("NumberGuesser cannot run the CompareGuess() method if the Target number has not been set."); }

        // If the guess was correct
        if (_target == _guess)
        {
            return GuessComparison.EqualGuess;
        }
        // If the guess was higher than the target
        else if (_target < _guess)
        {
            return GuessComparison.HigherGuess;
        }
        // If the guess was lower than the target
        else
        {
            return GuessComparison.LowerGuess;
        }
    }

    /// <summary>
    /// Checks if the player's feedback matches the real comparison between the target and the NumberGuesser's guess
    /// </summary>
    /// <param name="feedback">What the player answered in regards to the NumberGuesser's guess.</param>
    /// <returns>Whether or not the player's feedback matches the real comparison between the target and the NumberGuesser's guess.</returns>
    public bool ValidateFeedback(GuessComparison feedback)
    {
        // Error message in case the Target has not been set yet
        if (!_targetIsSet) { Debug.LogError("NumberGuesser cannot run the ValidateFeedback() method if the Target number has not been set."); }

        // Gets the actual comparison between the target and the NumberGuesser's guess
        GuessComparison comparison = CompareGuess();
        // Returns if the player's feedback matched the real comparison
        return comparison == feedback;
    }

    /// <summary>
    /// Sets the upperRange value of the NumberGuesser's guessing range
    /// </summary>
    /// <param name="upperRange">The value to be used as the new upperRange. This value is inclusive in the guessing range.</param>
    public void SetUpperRange(int upperRange)
    {
        _upperRange = upperRange; 
    }

    /// <summary>
    /// Resets the NumberGuesser so it can be used to guess a new number
    /// </summary>
    /// <param name="upperRange">The upperRange the reset Guesser should use.</param>
    public void ResetGuesser(int upperRange = DEFAULT_UPPER)
    {
        _upperRange = upperRange;       // Sets the upperRange
        _lowerRange = DEFAULT_LOWER;    // Resets the lowerRange
        _totalGuesses = NO_GUESSES;     // Resets totalGuesses counter to 0
        _targetIsSet = false;           // Marks that the target is not set, so the NumberGuesser doesn't accidentally use the old target
    }

    /// <summary>
    /// Function that should be called if the most recent guess was incorrect
    /// </summary>
    public void IncorrectGuess()
    {
        // Error message in case the Target has not been set yet
        if (!_targetIsSet) { Debug.LogError("NumberGuesser cannot run the IncorrectGuess() method if the Target number has not been set."); }

        switch (CompareGuess())
        {
            case GuessComparison.LowerGuess:
                // Changes the lowerRange to be the next integer higher than the guess
                //      Since the _guess was deemed too low, then both the guess and anything below it is out of range.
                _lowerRange = _guess + 1;
                break;

            case GuessComparison.HigherGuess:
                // Changes the upperRange to be the next integer lower than the guess
                //      Since the _guess was deemed too high, then both the guess and anything above it is out of range.
                _upperRange = _guess - 1;
                break;
            // Error message incase the IncorrectGuess function was incorrectly called
            //      This only occurs if the comparison was EqualGuess
            default:
                Debug.LogError("ERROR: IncorrectGuess was called, but the guess was labelled as correct by CompareGuess()");
                return;
        }
    }

    /// <summary>
    /// Checks if number is in range of the current lower and upper range.
    /// </summary>
    /// <param name="number">The number being checked.</param>
    /// <returns></returns>
    public bool IsInRange(int number)
    {
        // Checks if number was within the range by comparing to lowerRange and upperRange
        if (_lowerRange <= number && number <= _upperRange)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
}
