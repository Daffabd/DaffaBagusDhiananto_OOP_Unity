using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang collide memiliki tag yang sama
        if (other.CompareTag(gameObject.tag))
        {
            return;
        }

        // Cek apakah objek yang collide memiliki HitboxComponent
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Jika hitbox ditemukan, berikan damage
            hitbox.Damage(damage);
        }
    }
}
