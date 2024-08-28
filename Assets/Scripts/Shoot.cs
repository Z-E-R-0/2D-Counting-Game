using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab of the projectile to be shot
    public Transform firePoint;          // The point from which the projectile will be fired
    public float projectileSpeed = 20f;  // Speed at which the projectile will be shot

    public void Fire(float dirx)
    {
        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Apply velocity in the firePoint's right direction (X-axis direction)
        rb.velocity = firePoint.right* dirx * projectileSpeed;

        // Optional: Destroy the projectile after a certain time to avoid clutter
        Destroy(projectile, 5f);
    }
}
