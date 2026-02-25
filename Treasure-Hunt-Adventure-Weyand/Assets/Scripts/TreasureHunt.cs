using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TreasureHunt : MonoBehaviour
{
    // public variable linked to the reload display
    public GameObject ReloadPanel;
    // 'stepsTaken' is the number of steps taken
    private int _stepsTaken = 0;
    // 'isDead' to track whether the player is or is not alive
    private bool _isDead;

    // TODO:
    // Declare the following variables:
    // 'distanceToTreasure' to store how close the player is to the treasure (in meters)
    private float _distanceToTreasure;
    // 'hasKey' to track whether the player has found the key
    private bool _hasKey;
    // 'foundTreasure' to track whether the player has found the treasure chest
    private bool _foundTreasure;
    // 'clue' to hold the current hint message
    private string clue;

    // #####################################################
    // ###                                               ###
    // ###                NOTE FOR GRADER:               ###
    // ###                                               ###
    // #####################################################
    // I ADDED CONSTANTS TO REMOVE MAGIC NUMBERS

    const float FAR_DISTANCE = 15.5f;    // Represents the distance where the player is considered far from the treasure
    const float MIDDLE_DISTANCE = 10.5f; // Represents the distance where the player is considered not too far but not too close to the treasure
    const float CLOSE_DISTANCE = 5.5f;   // Represents the distance where the player is considered close to the treasure
    const float DIG_DISTANCE = 1.5f;     // Represents the minimum distance the player needs to be from the treasure in order to dig it up
    const float NO_DISTANCE = 0.0f;      // Represents the distance when the player is directly on top of the treasure


    private void Start()
    {
        // TODO:
        // Initialize your values
        // 'distanceToTreasure' to 15.5f
        _distanceToTreasure = FAR_DISTANCE;
        // Write an if statement that checks if 'hasKey' is false
        // if it is false, then set 'hasKey' to true
        if (!_hasKey) { _hasKey = true; }
    }

    private void Update()
    {
        if (!_isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Dig();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                UseKey();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                StepForward();
            }
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Coroutine reloadCoroutine = StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        ReloadPanel.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        ReloadPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    // PRIVATE FUNCTIONS
    private void Dig()
    {
        // TODO:
        // Write an if statement that checks if the distance to the treasure is less than 1.5f
        if (_distanceToTreasure < DIG_DISTANCE)
        {
            // if true, print "You found the treasure!" to the console and set 'foundTreasure' to TRUE
            Debug.Log("You found the treasure!");
            _foundTreasure = true;
        }
        // Otherwise, print "Alas, no treasure be found here!" to the console
        else
        {
            Debug.Log("Alas, no treasure be found here!");
        }
    }

    private void UseKey()
    {
        // TODO:
        // Write an if statement that checks if the player has the key and has found the treasure
        if (_hasKey && _foundTreasure)
        {
            // if both true, print "You've unlocked the treasure!  The booty be yers!" to the console
            Debug.Log("You've unlocked the treasure!  The booty be yers!");

        }
        // Otherwise, print "There be nothing to unlock!" to the console
        else
        {
            Debug.Log("There be nothing to unlock!");
        }
    }

    private void StepForward()
    {
        _stepsTaken++;

        // TODO:
        // Call CalculateDistanceToTreasure();
        CalculateDistanceToTreasure();

        DisplayClue();
    }

    private float CalculateDistanceToTreasure()
    {
        // TODO:
        // Set the 'distanceToTreasure' to equal 'distanceToTreasure' minus 'stepsTaken'
        _distanceToTreasure -= _stepsTaken;
        // return 'distanceToTreasure'
        return _distanceToTreasure;
    }

    private void DisplayClue()
    {

        // TODO:
        // Write an if statement that checks if the 'distanceToTreasure' is greater than 15.5f or is less than 0.0f
            // If the player is farther than the FAR_DISTANCE from the treasure, or has passed it
        if (_distanceToTreasure > FAR_DISTANCE || _distanceToTreasure < NO_DISTANCE)
        {
            // if true, set 'isDead' to True and clue equal to "Aye! Ye be going the wrong way!"
            _isDead = true;
            clue = "Aye! Ye be going the wrong way!";

        }
        // Write an else if statement that checks if the 'distanceToTreasure' is less than 15.5f and greater than or equal to 10.5f
            // If the player is within the FAR_DISTANCE, but not the MIDDLE_DISTANCE from the treasure
        else if (_distanceToTreasure < FAR_DISTANCE && _distanceToTreasure >= MIDDLE_DISTANCE)
        {
            // if true, set clue equal to "I sense ye be getting closer"
            clue = "I sense ye be getting closer";
        }
        // Write an else if statement that checks if the 'distanceToTreasure' is less than 10.5f and greater than or equal to 5.5f
            // If the player is within the MIDDLE_DISTANCE, but not the CLOSE_DISTANCE from the treasure
        else if (_distanceToTreasure < MIDDLE_DISTANCE && _distanceToTreasure >= CLOSE_DISTANCE)
        {
            // if true, set clue equal to "Aye, not too far now"
            clue = "Aye, not too far now";
        }
        // Write an else if statement that checks if the 'distanceToTreasure' is less than 5.5f and greater than or equal to 1.5f
            // If the player is within the CLOSE_DISTANCE, but not the DIG_DISTANCE from the treasure
        else if (_distanceToTreasure < CLOSE_DISTANCE && _distanceToTreasure >= DIG_DISTANCE)
        {
            // if true, set clue equal to "Almost there!  I can smell it!"
            clue = "Almost there!  I can smell it!";
        }
        // Otherwise, set clue equal to "Dig here! Dig here! We be at the spot on the map!"
            // The player is within the DIG_DISTANCE from the treasure
        else
        {
            clue = "Dig here! Dig here! We be at the spot on the map!";
        }
        // Debug.Log('clue') to the console
        Debug.Log(clue);
    }
}