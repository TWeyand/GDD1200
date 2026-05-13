using UnityEngine;
using UnityEngine.InputSystem;

public class MyInputs : MonoBehaviour
{
    public static MyInputs Instance { get; private set; }

    private SRInputs Inputs { get; set; }

    public static InputAction PlayerShoot { get; private set; }
    public static InputAction PlayerTurn { get; private set; }
    public static InputAction PlayerForward { get; private set; }
    public static InputAction PlayerPause { get; private set; }
    public static InputAction UIClose { get; private set; }
    public static InputAction UISelect { get; private set; }


    private void Awake()
    {
        // Singleton check
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Inputs = new SRInputs();

        PlayerShoot = Inputs.Player.Shoot;
        PlayerTurn = Inputs.Player.Turn;
        PlayerForward = Inputs.Player.Forward;
        PlayerPause = Inputs.Player.Pause;

        UIClose = Inputs.UI.Close;
        UISelect = Inputs.UI.Select;

        EnablePlayerActions();
        EnableUIActions();

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Disables the Player Inputs
    /// </summary>
    private void DisablePlayerActions()
    {
        Inputs.Player.Disable();
    }
    /// <summary>
    /// Enables the Player Inputs
    /// </summary>
    private void EnablePlayerActions()
    {
        Inputs.Player.Enable();
    }

    /// <summary>
    /// Disables the UI Inputs
    /// </summary>
    private void DisableUIActions()
    {
        Inputs.UI.Disable();
    }

    /// <summary>
    /// Enables the UI Inputs
    /// </summary>
    private void EnableUIActions()
    {
        Inputs.UI.Enable();
    }

    public void OnPause()
    {
        DisablePlayerActions();
        EnableUIActions();
    }

    public void OnUnpause()
    {
        EnablePlayerActions();
        DisableUIActions();
    }
}
