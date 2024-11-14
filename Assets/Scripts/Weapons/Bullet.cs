using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;
    public int damage = 10;
    private Rigidbody2D rb;

    // Reference to the object pool (set by the Weapon class when spawning bullets)
    public IObjectPool<Bullet> objectPool;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;  // Moves bullet upward by default
    }

    private void OnEnable()
    {
        // Reset the velocity each time the bullet is activated
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))  // Adjust the tag as needed
        {
            objectPool.Release(this);  // Return bullet to pool on collision
        }
    }

    private void OnBecameInvisible()
    {
        if (objectPool != null)
        {
            objectPool.Release(this);  // Return bullet to pool if it leaves the screen
        }
    }
}
