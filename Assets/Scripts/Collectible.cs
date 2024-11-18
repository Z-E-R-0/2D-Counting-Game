using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 10;  // Points the collectible gives to the player
    public GameManager gameManager;  // Reference to the GameManager
    // Reference to the AudioSource component
    [SerializeField] GameObject particlePrefab;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Check if the player is collecting the item
        {
            Collect();  // Call the collect function
        }
    }

    void Collect()
    {
        // Add points to the player's score
        gameManager.AddPoints(points);
        Debug.Log("Collected! Points added: " + points);
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        // Destroy the collectible object
        Destroy(gameObject);
    }
}
