using UnityEngine;

/// <summary>
/// The purpose of this script is to handle the animation for the player teleporting between rooms
/// </summary>
public class AnimationManager : MonoBehaviour
{
    public Animator TransitionAnimator; // the reference to the Animator component

    private string _isTeleporting = "_isTeleporting"; // converting the string value of the bool to a variable

    // Added so that there wouldn't be a string comparison each time "_isTeleporting" is needed
    private int _isTeleportingID = Animator.StringToHash("_isTeleporting");
    public void SetTeleportingAnimationBool(bool value)
    {
        //TODO
        // Call the SetBool() function (the bool we are setting is _isTeleporting) on the TransitionAnimator
        //      Set the bool value to equal the "value" argument we are passing through
        // Hint:  https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Animator.SetBool.html
        TransitionAnimator.SetBool(_isTeleportingID, value);
    }
}
