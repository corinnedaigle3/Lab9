using UnityEngine;

public class Target : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;

    public void Initialize(float moveSpeed)
    {
        speed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();

        // Move continuously to the right
        rb.linearVelocity = Vector2.right * speed;

        // Destroy after 10 seconds
        Destroy(gameObject, 5f);
    }
}