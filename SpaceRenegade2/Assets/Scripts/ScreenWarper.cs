using UnityEngine;

public class ScreenWarper : MonoBehaviour
{
    /// <summary>
    ///  The boundaries of the screen
    /// </summary>
    private Vector2 _screenBounds;

    private void Start()
    {
        // Gets the bounds of the screen
        _screenBounds = GetScreenBounds(Camera.main);
    }

    private void Update()
    {
        ScreenWarp();
    }

    public void ScreenWarp()
    {
        var position = transform.position;

        // horizontal warp
        if (position.x > _screenBounds.x)
        {
            position.x = -_screenBounds.x;
        }
        else if (position.x < -_screenBounds.x)
        {
            position.x = _screenBounds.x;
        }

        // vertical warp
        if (position.y > _screenBounds.y)
        {
            position.y = -_screenBounds.y;
        }
        else if (position.y < -_screenBounds.y)
        {
            position.y = _screenBounds.y;
        }

        transform.position = position;
    }

    /// <summary>
    /// Gets the bounds of the screen
    /// </summary>
    /// <param name="cam">The active camera</param>
    public Vector2 GetScreenBounds(Camera cam)
    {
        // Gets the top right point of the screen
        var screenTopRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        // Returns the top right point of the screen
        return new Vector2(screenTopRight.x, screenTopRight.y);
    }
}
