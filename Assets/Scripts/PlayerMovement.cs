using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;      // Speed of the player movement
    public float jetpackForce = 10f;  // Force applied when the jetpack is active
    public float maxFuel = 100f;      // Maximum fuel capacity for the jetpack
    public float fuelConsumptionRate = 10f;  // Rate at which fuel is consumed while using the jetpack
    public float fuelRechargeRate = 5f;  // Rate at which fuel recharges when grounded
    public float groundCheckDistance = 0.1f; // Distance for raycast to check for ground
    public LayerMask groundLayer;     // Layer mask to specify which layer is considered as ground

    private Rigidbody2D rb;           // Reference to the Rigidbody2D component
    private bool isGrounded;          // Flag to check if the player is on the ground
    private Shoot shootScript;        // Reference to the Shoot component
    private float moveInput;          // Variable to store horizontal input
    private float currentFuel;        // Current amount of fuel
    private bool isJetpacking;
    private bool isMoving;
    [SerializeField] private Transform jetPackParticel;// Flag to check if the player is using the jetpack
    [SerializeField] AudioSource jetPackAudio;
    [SerializeField] AudioSource movingAudio;
    [SerializeField] Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootScript = GetComponent<Shoot>();  // Get the Shoot component attached to the same GameObject
        currentFuel = maxFuel;  // Initialize fuel to maximum at the start
        
    }

    void Update()
    {
        animator.SetBool("IsJetpacking", isJetpacking);

       
       
             
        animator.SetBool("isWalking", isMoving);

       
        Move();
        CheckGrounded();
        HandleShooting();  // Handle shooting input
    }
    private void FixedUpdate()
    {
        HandleJetpack();
    }

    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal");  // Get horizontal input (A/D keys or Left/Right arrow keys)
        isMoving = Mathf.Abs(moveInput) > 0;
        // Apply velocity based on input
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Optional: Flip player sprite based on direction
        if (moveInput > 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (isMoving && !movingAudio.isPlaying && !isJetpacking)
        {
            movingAudio.Play();
        }
        else if (!isMoving && movingAudio.isPlaying)
        {
            movingAudio.Stop();
        }
    }

    private void HandleJetpack()
    {
        // If the player is pressing the jump button and has fuel, activate the jetpack
        if (Input.GetButton("Jump") && currentFuel > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jetpackForce);
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            isJetpacking = true;
            
        }
        else
        {

            isJetpacking = false;
        }
        if (isJetpacking && !jetPackAudio.isPlaying)
        {
            jetPackAudio.Play();
        }
        else if (!isJetpacking && jetPackAudio.isPlaying)
        {
            jetPackAudio.Stop();
        }
        HandelJetPackParticle();
        // Recharge fuel when the player is grounded and not using the jetpack
        if (isGrounded && !isJetpacking)
        {
            currentFuel += fuelRechargeRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);  // Ensure fuel does not exceed max capacity
        }

    }

    private void CheckGrounded()
    {
        // Cast a ray downwards from the player's position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        // Check if the raycast hits something on the ground layer
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // Optional: Visualize the raycast in the Scene view
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    private void HandleShooting()
    {
        // Check if the player presses the shoot button (e.g., "Fire1" is left Ctrl or mouse button by default)
        if (Input.GetButtonDown("Fire1"))
        {
            float dirx = transform.localScale.x;  // Determine the shooting direction based on the player's facing direction
            shootScript.Fire(-dirx);  // Call the Fire method from the Shoot script with direction
        }
    }

    private void HandelJetPackParticle()
    {
        if (isJetpacking)
        {

            jetPackParticel.gameObject.SetActive(true);

        }
        else
        {
            jetPackParticel.gameObject.SetActive(false);


        }


    }
}
