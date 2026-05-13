using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TurnTowardsPlayer();
    }

    private void TurnTowardsPlayer()
    {
        if (PlayerManager.Player != null)
        {
            // Get the direction of the enemy to the player
            var dir = PlayerManager.Player.transform.position - transform.position;

            // Get the angle using atan, and convert from radians to degrees
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(0, 0, (angle));
        }
    }
}
