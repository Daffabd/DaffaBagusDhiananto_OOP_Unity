using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 3f;             // Kecepatan gerak musuh
    public Weapon bossWeapon;
    public float shootInterval = 2f;    // Interval menembak
    private float shootTimer;

    private Vector2 screenBounds;       // Batas layar
    private Vector2 direction;          // Arah gerak horizontal

    private void Start()
    {
        // Mendapatkan batas layar
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Spawn musuh di posisi acak di sisi atas layar
        float spawnSide = Random.Range(0, 2) == 0 ? -1f : 1f;
        transform.position = new Vector3(spawnSide * screenBounds.x, Random.Range(0, 4f), 0);

        // Atur arah gerakan horizontal
        direction = new Vector2(spawnSide, 0);

        // Reset timer tembak
        shootTimer = shootInterval;
    }

    private void Update()
    {
        // Gerakan horizontal
        transform.Translate(direction * speed * Time.deltaTime);

        // Balik arah jika mencapai batas layar
        if (transform.position.x < -screenBounds.x || transform.position.x > screenBounds.x)
        {
            direction = -direction; // Balik arah
        }

        // Timer untuk menembak
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval; // Reset timer
        }
    }

    private void Shoot()
    {
        if (bossWeapon != null)
        {
            bossWeapon.Shoot();
        }
    }
}