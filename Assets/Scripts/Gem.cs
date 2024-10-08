using UnityEngine;
using TMPro;

public class Gem : MonoBehaviour
{
    public int gemValue; // The value of the gem (e.g., 0, 1, 2)
    public GameManager gameManager;
    public GameObject particlePrefab; // Assign a particle prefab instead of just an object

    private TextMeshProUGUI valueText; // TextMeshPro component for displaying the gem's value

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>(); // Find the GameManager in the scene
    }

    private void Start()
    {
        // Get the TextMeshProUGUI component from the first child (assuming the first child is the text object)
        valueText = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (valueText != null)
        {
            valueText.text = gemValue.ToString(); // Set the text to display the gem's value
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on the gem's child object.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) // Assuming the projectile has the "Projectile" tag
        {
            CheckAnswer();
            Destroy(other.gameObject); // Destroy the projectile when it hits the gem
        }
    }

    void CheckAnswer()
    {
        // Compare gem value with the correct answer in the GameManager
        if (gameManager.CheckAnswer(gemValue))
        {
            Debug.Log("Correct gem hit! Generating new question...");
        }
        else
        {
            Debug.Log("Wrong gem hit!");
        }

        // Spawn the particle effect for both correct and incorrect answers
        if (particlePrefab != null)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity); // Spawn particle prefab at gem's position
        }

        // Delay the destruction of the gem
       
    }
}
