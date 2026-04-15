using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The current phase of the game. 
    /// This System is sometimes used to delay certain phases from starting until the next update call.
    /// However it is mostly used to help me internally track what the current phase of the game is for debugging purposes.
    /// </summary>
    enum GamePhases
    {
        OutOfGameLoop,      // When out of the game loop (EX: MainMenu or EndGame scene)

        StartRound,         // When the round is just starting/initializing
        GetPlayerNumber,    // While the player is entering their number
        StartGuessing,      // The start of the guessing phase
        Guessing,           // Waiting for the player to enter guess feedback
        EndRound,           // When the correct guess has been made
    }

    /// <summary>
    /// The static instance of the GameManager. Exists so any object has access to the game manager.
    /// </summary>
    public static GameManager instance;
    /// <summary>
    /// An Extension of the GameManager that stores most of the Events that will be needed to be accessed broadly across the game.
    /// </summary>
    private MyEvents _myEvents;
    /// <summary>
    /// An Extension of the GameManager that stores most of the Events that will be needed to be accessed broadly across the game.
    /// </summary>
    public MyEvents MyEvents { get { return _myEvents; } set { _myEvents = value; } }
    /// <summary>
    /// The current Phase of the game loop.
    /// See the above GamePhases enum for more details
    /// </summary>
    [SerializeField]
    private GamePhases GamePhase = GamePhases.OutOfGameLoop;

    
    
    
    private void Awake() {
        // If there is no GameManager.instance
        if (GameManager.instance == null)
        {
            // Marks this GameManager as the static instance since it is the first GameManager to be created
            GameManager.instance = this;

            // Marks the GameManager GameObject as persistent so it isn't destroyed across scenes
            //      The GameManager needs to persist across scenes due to how it's logic works
            DontDestroyOnLoad(GameManager.instance.gameObject);

            #region Ensures that the MyEvents object is assigned.
            /*      If it isn't, attempts to find the MyEvent Component or creates a MyEvents Component,
             *      because the game cannot function without the MyEvents object.
             */

            // Attempts to find MyEvents
            _myEvents = GetComponent<MyEvents>();

            // If MyEvents was not found, creates MyEvents instead
            if (_myEvents == null)
            {
                // Adds MyEvents to the GameManager GameObject and stores it in the MyEvents variable
                _myEvents = gameObject.AddComponent<MyEvents>();
            }
            #endregion

            // Subscribes to needed events
            //  *** NOTE:
            //    * Technically I could have just manually put the logic into the Event Invoking functions,
            //    * since the MyEvents object and GameManager are directly coupled anyways.
            //    * I didn't do that, because I didn't think about it until later.
            MyEvents.EndPlayerNumberPhaseEvent.AddListener(OnEndPlayerNumberPhase);
            MyEvents.RestartGuessingPhaseEvent.AddListener(OnRestartGuessingPhase);
            MyEvents.EndRoundEvent.AddListener(OnEndRound);
        }
        // If there already is a GameManager.instance
        //      Also occurs if this is the GameManager.instance, but that is impossible
        else
        {
            // Logs that the GameManager already exists and WAS NOT overidden
            Debug.Log($"Tried to create Game manager called {this.gameObject.name}," +
                      $"\n but there is already a Game manager called {GameManager.instance.gameObject.name}");
            // Destroys objeect containing redundant GameManager
            Destroy(this.gameObject);
        }
    }

    // Clears event subscriptions when object is destroyed
    private void OnDestroy()
    {
        if (MyEvents != null)
        {
            /* Attempts to Unsubscribe from all events
             *      This will not cause any issues even if the MyEvents object is already deleted
             *      so checking for that is unneeded */
            UnsubscribeFromMyEvents();
        }
    }

    /// <summary>
    /// Unsubscribes from all events connected to the MyEvents object.
    /// Unsubscribing from events is important to protect memory leaks.
    /// </summary>
    private void UnsubscribeFromMyEvents()
    {
        MyEvents.EndPlayerNumberPhaseEvent.RemoveListener(OnEndPlayerNumberPhase);
        MyEvents.RestartGuessingPhaseEvent.RemoveListener(OnRestartGuessingPhase);
        MyEvents.EndRoundEvent.RemoveListener(OnEndRound);
    }

    private void Update()
    {
        // Runs the logic for the GamePhase
        PhaseHandler();
    }

    // Logic for each GamePhase
    private void PhaseHandler()
    {
        switch (GamePhase)
        {
            // Not inside of the GameLoop (EX: MainMenu)
            case GamePhases.OutOfGameLoop:
                return;

            // Start of the round logic
            case GamePhases.StartRound:
                // Invokes the StartRoundEvent so that the round starts
                MyEvents.StartPlayerNumberPhase();
                GamePhase = GamePhases.GetPlayerNumber;
                return;

            // Waiting to GetPlayerNumber from the player
            case GamePhases.GetPlayerNumber:
                return;

            // Start of a Guessing phase
            case GamePhases.StartGuessing:
                GamePhase = GamePhases.Guessing;
                MyEvents.StartGuessingPhase();
                return;

            // Waiting for the player's feedback to the guess
            case GamePhases.Guessing:
                return;

            // End of round
            case GamePhases.EndRound:
                return;

            // INVALID: GamePhase is not properly set
            default:
                Debug.LogError("ERROR IN GAME MANAGER: GamePhase is not set to a valid GamePhase");
                break;
        }
    }

    /// <summary>
    /// Used to initialize a new round.
    /// Resets anything that needs to be reset, then starts the round.
    /// </summary>
    public void InitializeRound()
    {
        MyEvents.ResetRound();
        GamePhase = GamePhases.StartRound;  
    }

    /// <summary>
    /// Used to end the scene that contains the guessing game.
    /// </summary>
    public void EndGuessingGame()
    {
        GamePhase = GamePhases.OutOfGameLoop;
        GetComponent<SceneHandler>().LoadEndScene();
    }

    #region Event Listeners
    public void OnEndPlayerNumberPhase()
    {
        GamePhase = GamePhases.StartGuessing;
    }

    public void OnRestartGuessingPhase()
    {
        GamePhase = GamePhases.StartGuessing;
    }

    public void OnEndRound()
    {
        GamePhase = GamePhases.EndRound;
    }
    #endregion
}
