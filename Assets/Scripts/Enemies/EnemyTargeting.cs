using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f; // Kecepatan gerak musuh
    private Transform player;

    private void Start()
    {
        // Cari Player di scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Spawn musuh di posisi acak di luar layar
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(
            Random.Range(-screenBounds.x, screenBounds.x),
            screenBounds.y + 1f,
            0
        );
    }

    private void Update()
    {
        // Jika Player ada, bergerak menuju ke arahnya
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jika menyentuh Player, musuh mati
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
