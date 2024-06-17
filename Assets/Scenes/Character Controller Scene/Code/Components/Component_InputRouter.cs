using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


//  This component essentially does what the PlayerInput standard component does - takes the player input and
//  broadcasts it as an event. We take the callbacks from the PlayerInput component and perform some additional
//  logic here, however, such as including a logic gate for if the player input should be "turned off," and filter
//  the callback contexts into more usable data before passing them out as events
public class Component_InputRouter : MonoBehaviour
{
    #region Flags & Definitions
    //the primary flag for checking if player input of any kind is allowed. disable this if you want none of the 
    //input actions to work (movement, pause, etc). Note that this will not disable mouse-driven UI events like UI buttons
    //can be toggled in the Inspector for testing purposes, starts in false position to allow for opening cutscenes, etc
    public bool isAllowingPlayerInput = false;
    #endregion

    #region Events
    //this is the event created when Vector2 movement input is allowed to pass from the router
    public Vector2UnityEvent onCharacterMovementInput;
    public UnityEvent onCharacterMovementCanceled;
    #endregion

    #region Input Action Accept Methods
    //These methods are where the context from the PlayerInput component is accepted and processed - these methods
    //can be connected to the PlayerInput UnityEvent callbacks in the Inspector
    public void AcceptCharacterMovementInput(InputAction.CallbackContext context)
    {
        if (!CheckAllowingPlayerInput()) return;

        if (context.performed) onCharacterMovementInput?.Invoke(context.ReadValue<Vector2>());
        if (context.canceled) onCharacterMovementCanceled?.Invoke();
    }
    #endregion

    #region On/Off Functionality
    //It may be unnessary to make this check a method instead of simply check the value of the bool directly, 
    //but someone may need to add some logic here later
    private bool CheckAllowingPlayerInput()
    {
        if (isAllowingPlayerInput) return true;
        else return false;
    }

    //Calling this turns on and off the state of allowing player input, and invokes cancel events where applicable if turning off
    // - there's probably a more elegant way of doing this but this is how it is at the moment
    public void TogglePlayerInputAllowed() 
    {
        if (!isAllowingPlayerInput) isAllowingPlayerInput = true;
        else
        {
            isAllowingPlayerInput = false;
            onCharacterMovementCanceled?.Invoke();
        }

    }
    #endregion
}
