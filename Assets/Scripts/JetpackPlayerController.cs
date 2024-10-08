using UnityEngine;

public class JetpackPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jetpackForce = 10f;
    public float maxJetpackHeight = 15f;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Animator animator;

    public GameObject jetpackParticles; // Reference to the jetpack particle effect

    private bool isGrounded;
    private bool jetpackActive = false;
    private bool isWalking = false;  // Bool to track walking state
    public bool facingRight = true;  // Track the direction the player is facing

    void Update()
    {
        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("IsJetpacking", jetpackActive);
        animator.SetBool("isWalking", isWalking);  // Update the walking state in the animator

        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Check if the player is walking (i.e., moving horizontally)
        if (Mathf.Abs(moveInput) > 0 && isGrounded)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // Flip the player based on movement direction
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Jetpack control and particle effect
        if (Input.GetButton("Jump") && transform.position.y < maxJetpackHeight)
        {
            rb.velocity = new Vector2(rb.velocity.x, jetpackForce);
            jetpackActive = true;
            jetpackParticles.SetActive(true);  // Enable the particle effect
        }
        else
        {
            jetpackActive = false;
            jetpackParticles.SetActive(false); // Disable the particle effect
        }
    }

    // Flip the player's sprite horizontally
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Flip the x-axis of the player's local scale
        transform.localScale = localScale;
    }
}
