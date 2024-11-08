using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private Vector2 cameraBounds;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Hitung kecepatan dan friksi
        moveVelocity = new Vector2(
            2 * maxSpeed.x / timeToFullSpeed.x,
            2 * maxSpeed.y / timeToFullSpeed.y
        );

        moveFriction = new Vector2(
            2 * maxSpeed.x / Mathf.Pow(timeToFullSpeed.x, 2),
            2 * maxSpeed.y / Mathf.Pow(timeToFullSpeed.y, 2)
        );

        stopFriction = new Vector2(
            2 * maxSpeed.x / Mathf.Pow(timeToStop.x, 2),
            2 * maxSpeed.y / Mathf.Pow(timeToStop.y, 2)
        );

        // Tentukan batas kamera
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        cameraBounds = new Vector2(cameraWidth, cameraHeight);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(inputX, inputY).normalized;

        Vector2 currentVelocity = rb.velocity;
        Vector2 targetVelocity = new Vector2(moveDirection.x * moveVelocity.x, moveDirection.y * moveVelocity.y);

        Vector2 friction = GetFriction(currentVelocity);
        rb.velocity = new Vector2(
            Mathf.Lerp(currentVelocity.x, targetVelocity.x, friction.x * Time.fixedDeltaTime),
            Mathf.Lerp(currentVelocity.y, targetVelocity.y, friction.y * Time.fixedDeltaTime)
        );

        // Batasi pergerakan pesawat dalam batas kamera
        ClampPosition();
    }

    private Vector2 GetFriction(Vector2 currentVelocity)
    {
        return new Vector2(
            Mathf.Abs(currentVelocity.x) > stopClamp.x ? moveFriction.x : stopFriction.x,
            Mathf.Abs(currentVelocity.y) > stopClamp.y ? moveFriction.y : stopFriction.y
        );
    }

    private void ClampPosition()
    {
        // Dapatkan posisi pesawat
        Vector3 position = transform.position;

        // Tentukan batas posisi x dan y sesuai dengan batas kamera
        position.x = Mathf.Clamp(position.x, -cameraBounds.x, cameraBounds.x);
        position.y = Mathf.Clamp(position.y, -cameraBounds.y, cameraBounds.y);

        // Perbarui posisi pesawat jika melampaui batas
        transform.position = position;
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0;
    }
}
