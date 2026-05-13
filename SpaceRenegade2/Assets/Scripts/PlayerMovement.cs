using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private ShipMovement _shipMovement;

    private Vector2 _screenBounds;

    private void OnEnable()
    {
        MyInputs.PlayerTurn.performed += OnTurnPerformed;
        MyInputs.PlayerTurn.canceled += OnTurnCanceled;

        MyInputs.PlayerForward.performed += OnForwardPerformed;
        MyInputs.PlayerForward.canceled += OnForwardCanceled;
    }

    private void OnDisable()
    {
        MyInputs.PlayerTurn.performed -= OnTurnPerformed;
        MyInputs.PlayerTurn.canceled -= OnTurnCanceled;

        MyInputs.PlayerForward.performed -= OnForwardPerformed;
        MyInputs.PlayerForward.canceled -= OnForwardCanceled;
    }

    public void OnTurnPerformed(InputAction.CallbackContext ctx)
    {
        _shipMovement.TurnValue = ctx.ReadValue<float>();
        Debug.Log($"Turn Performed: value = {_shipMovement.TurnValue}");
    }

    public void OnTurnCanceled(InputAction.CallbackContext ctx)
    {
        _shipMovement.TurnValue = 0.0f;
        Debug.Log($"Turn Canceled");
    }

    public void OnForwardPerformed(InputAction.CallbackContext ctx)
    {
        _shipMovement.ThrustValue = true;
        Debug.Log($"Forward Performed: value = {_shipMovement.ThrustValue}");
    }

    public void OnForwardCanceled(InputAction.CallbackContext ctx)
    {
        _shipMovement.ThrustValue = false;
        Debug.Log($"Forward Canceled");
    }
}
