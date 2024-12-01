using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 5f;

    private Vector2 vertical;

    private Rigidbody2D rb;
    private void Start()
    {

        float spawnSide = Random.Range(-9, 9);
        vertical = new Vector2(spawnSide, 0);

        transform.position = new Vector2(spawnSide, 6);
        rb.velocity = Vector2.down * speed;
        
    }
    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Destroy the enemy if it goes off the bottom of the screen
        if (transform.position.y < -5) // Adjust screen boundaries as needed
        {
            float spawnSide = Random.Range(-9, 9);
            vertical = new Vector2(spawnSide, 0);
            transform.position = new Vector2(spawnSide, 6);
            rb.velocity = Vector2.down * speed;
        }
    }
}

