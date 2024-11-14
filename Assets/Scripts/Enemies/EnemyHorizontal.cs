using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f; // Kecepatan gerak musuh
    private Vector2 screenBounds;
    private bool movingRight;

    private void Start()
    {
        // Mendapatkan batas layar berdasarkan kamera utama
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Tentukan spawn secara acak di kiri atau kanan
        if (Random.value < 0.5f)
        {
            transform.position = new Vector3(-screenBounds.x - 1f, Random.Range(-screenBounds.y, screenBounds.y), 0);
            movingRight = true; // Bergerak ke kanan
        }
        else
        {
            transform.position = new Vector3(screenBounds.x + 1f, Random.Range(-screenBounds.y, screenBounds.y), 0);
            movingRight = false; // Bergerak ke kiri
        }
    }

    private void Update()
    {
        // Pergerakan horizontal
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        // Cek apakah sudah keluar layar, jika iya ganti arah
        if (transform.position.x > screenBounds.x + 1f)
        {
            movingRight = false; // Bergerak ke kiri
        }
        else if (transform.position.x < -screenBounds.x - 1f)
        {
            movingRight = true; // Bergerak ke kanan
        }
    }
}
