using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 5f; // Kecepatan gerak musuh
    private Vector2 screenBounds;

    private void Start()
    {
        // Mendapatkan batas layar berdasarkan kamera utama
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Spawn musuh di atas layar secara horizontal acak
        transform.position = new Vector3(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + 1f, 0);
    }

    private void Update()
    {
        // Pergerakan ke bawah
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Jika musuh keluar dari layar, hancurkan objek
        if (transform.position.y < -screenBounds.y - 1f)
        {
            Destroy(gameObject);
        }
    }
}
