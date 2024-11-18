using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    private bool facingRight = true;  // Keep track of the player's facing direction
    public JetpackPlayerController jetpackPlayer;
    public AudioSource shootAudio;
    void Update()
    {
        if (Input.GetButtonDown("Fire1")||Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
           
            {
                shootAudio.Play();// Enable the particle effect

            }
        }
       
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Projectile script and set its direction based on player's facing direction
        float direction = jetpackPlayer.facingRight ? 1f : -1f;  // If facing right, move right, else move left
        projectile.GetComponent<Projectile>().SetDirection(direction);
    }

    // This function should be called when the player flips direction
    public void Flip(bool isFacingRight)
    {
        facingRight = isFacingRight;
    }
}
