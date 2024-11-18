using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float direction = 1f; // Default to moving right

    // This method will set the projectile direction when instantiated
    public void SetDirection(float facingDirection)
    {
        direction = facingDirection;
    }

    void Start()
    {
        // Apply velocity in the direction the player is facing
        rb.linearVelocity = transform.right * speed * direction;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Add impact logic here (destroy gem, create effect, etc.)
        Destroy(gameObject);
    }
}
