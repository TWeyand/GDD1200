using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceDelete : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        bool deactivate = Keyboard.current.spaceKey.isPressed;
        if (deactivate)
        {
            Destroy(gameObject);
        }
    }
}
