using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    /// <summary>
    /// Death function used to connect to UnityEvents in the editor.
    /// Tells the enemy manager that this enemy has died.
    /// </summary>
    public void OnDie()
    {
        EnemyManager.Instance.EnemyDied(gameObject);
    }
}
