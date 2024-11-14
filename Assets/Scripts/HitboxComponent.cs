using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitboxComponent : MonoBehaviour
{
    // Properti untuk menyimpan HealthComponent
    public HealthComponent health;

    // Overloading method Damage
    public void Damage(int damage)
    {
        if (health != null)
        {
            health.Subtract(damage);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (health != null)
        {
            health.Subtract(bullet.Damage);
        }
    }
}
