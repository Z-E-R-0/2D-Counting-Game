using UnityEngine;
using TMPro;
using System.Collections;

public class Gem : MonoBehaviour
{
    public int gemValue;
    public GameManager gameManager;
    public GameObject particlePrefab;
    public GameObject particlePrefabCorrect;
    public Color correctColor = Color.green;
    public Color wrongColor = Color.red;
    private TextMeshProUGUI valueText;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        valueText = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        if (valueText != null)
        {
            valueText.text = gemValue.ToString();
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found on the gem's child object.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            CheckAnswer();
            Destroy(other.gameObject);
            
        }
    }

    void CheckAnswer()
    {
        // Compare gem value with the correct answer in the GameManager
        if (gameManager.CheckAnswer(gemValue))
        {
            Debug.Log("Correct gem hit! Generating new question...");
            StartCoroutine(BlinkEffect(true));

        }
        else
        {
            Debug.Log("Wrong gem hit!");
            StartCoroutine(BlinkEffect(false));
            gameManager.PlayBlastSound();
        }

        // Spawn the particle effect for both correct and incorrect answers
        if (particlePrefab != null)
        {
           // Instantiate(particlePrefab, transform.position, Quaternion.identity); // Spawn particle prefab at gem's position
        }
    }

    IEnumerator BlinkEffect(bool isCorrect)
    {
        // Set the gem's color based on the answer
        spriteRenderer.color = isCorrect ? correctColor : wrongColor;

        // Blink effect
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = Color.white;

        // Spawn particles if available
        if (particlePrefab != null && !isCorrect)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
        if (particlePrefab != null && isCorrect)
        {
            Instantiate(particlePrefabCorrect, transform.position, Quaternion.identity);
        }

        // Destroy the gem if the answer was correct
        if (isCorrect)
        {
            Destroy(gameObject);
        }
    }
}
