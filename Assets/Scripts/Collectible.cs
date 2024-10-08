using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 10;  // Points or value the collectible gives to the player
    public GameManager gameManager;  // Reference to the GameManager or score handler

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Check if the player is collecting the item
        {
            Collect();  // Call the collect function
        }
    }

    void Collect()
    {
        // Add points to the player's score or trigger any other event
        gameManager.AddPoints(points);  // Assuming the GameManager handles score

        // Optional: Play sound, trigger particle effect, etc.
        Debug.Log("Collectible picked up! Points: " + points);

        // Destroy the collectible object
        Destroy(gameObject);
    }
}
