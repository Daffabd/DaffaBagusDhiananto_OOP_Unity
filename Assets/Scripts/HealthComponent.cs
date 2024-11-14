using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth;
    private int health;

    // Getter untuk properti health
    public int Health => health;

    // Setter untuk mengurangi nilai health
    public void Subtract(int damage)
    {
        health -= damage;

        // Jika health <= 0, panggil Destroy
        if (health <= 0)
        {
            Destroy();
        }
    }

    private void Start()
    {
        // Inisialisasi health dengan nilai maxHealth
        health = maxHealth;
    }

    // Method untuk menghancurkan Object
    private void Destroy()
    {
        Debug.Log($"{gameObject.name} has been destroyed!");
        Destroy(gameObject);
    }
}
