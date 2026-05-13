using UnityEditor.Build.Content;
using UnityEditor.PackageManager;
using UnityEngine;

public class Health : MonoBehaviour
{

    [field: SerializeField] public int HP { get; private set; } = 3;

    public UnityEngine.Events.UnityEvent DeathEvent;
    public UnityEngine.Events.UnityEvent DamageEvent;

    public void Damage(int damageAmount = 1)
    {
        if (!enabled) return;

        HP -= damageAmount;

        if (HP <= 0)
        {
            Die();
        }
        else
        {
            DamageEvent?.Invoke();
        }
    }

    private void Die()
    {
        DeathEvent?.Invoke();
    }
}
